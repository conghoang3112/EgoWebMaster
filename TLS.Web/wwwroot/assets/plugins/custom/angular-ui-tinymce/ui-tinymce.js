/**
 * Binds a TinyMCE widget to <textarea> elements.
 */
angular.module('ui.tinymce', [])
	.value('uiTinymceConfig', {})
	.directive('uiTinymce', ['$rootScope', '$compile', '$timeout', '$window', '$sce', 'uiTinymceConfig', function ($rootScope, $compile, $timeout, $window, $sce, uiTinymceConfig) {
		uiTinymceConfig = uiTinymceConfig || {};
		var ID_ATTR = 'ui-tinymce';

		if (uiTinymceConfig.baseUrl) {
			tinymce.baseURL = uiTinymceConfig.baseUrl;
		} else {
			tinymce.baseURL = "/admin-assets/plugins/tinymce/";
		}

		return {
			require: ['ngModel', '^?form'],
			priority: 599,
			link: function (scope, element, attrs, ctrls) {
				if (!$window.tinymce) {
					return;
				}

				var ngModel = ctrls[0],
					form = ctrls[1] || null;

				var expression, options = {
					debounce: true
				}, tinyInstance,
					updateView = function (editor) {
						var content = editor.getContent({ format: options.format }).trim();
						content = $sce.trustAsHtml(content);
						ngModel.$setViewValue(content);
						if (!$rootScope.$$phase) {
							scope.$digest();
						}
					};

				function toggleDisable(disabled) {
					if (disabled) {
						ensureInstance();

						if (tinyInstance) {
							tinyInstance.getBody().setAttribute('contenteditable', false);
						}
					} else {
						ensureInstance();

						if (tinyInstance && !tinyInstance.settings.readonly) {
							tinyInstance.getBody().setAttribute('contenteditable', true);
						}
					}
				}

				// generate an ID
				if (angular.isUndefined(attrs.id)) {
					attrs.$set('id', ID_ATTR + '-' + (new Date().valueOf()));
				}

				expression = {};

				angular.extend(expression, scope.$eval(attrs.uiTinymce));

				//Debounce update and save action
				var debouncedUpdate = (function (debouncedUpdateDelay) {
					var debouncedUpdateTimer;
					return function (ed) {
						$timeout.cancel(debouncedUpdateTimer);
						debouncedUpdateTimer = $timeout(function () {
							return (function (ed) {
								if (ed.isDirty()) {
									ed.save();
									updateView(ed);
								}
							})(ed);
						}, debouncedUpdateDelay);
					};
				})(400);

				var setupOptions = {
					// Update model when calling setContent
					// (such as from the source editor popup)
					setup: function (ed) {
						ed.on('init', function () {
							ngModel.$render();
							ngModel.$setPristine();
							ngModel.$setUntouched();
							if (form) {
								form.$setPristine();
							}
						});

						ed.on('FullscreenStateChanged', function (e) {
							if (e.state) {							
								$("div.tox-fullscreen").addClass("cth-tinymce-fullscreen");
								//$('.modal-dialog').attr('style', 'transform: none !important;-webkit-transform: none;-ms-transform: none;-o-transform: none;transform: none;');
							} else {
								
							}
							$("html").removeClass("tox-fullscreen");
							$("body").removeClass("tox-fullscreen");
						});

						// Update model when:
						// - a button has been clicked [ExecCommand]
						// - the editor content has been modified [change]
						// - the node has changed [NodeChange]
						// - an object has been resized (table, image) [ObjectResized]
						ed.on('ExecCommand change NodeChange ObjectResized', function () {
							if (!options.debounce) {
								ed.save();
								updateView(ed);
								return;
							}
							debouncedUpdate(ed);
						});

						ed.on('blur', function () {
							element[0].blur();
							ngModel.$setTouched();
							if (!$rootScope.$$phase) {
								scope.$digest();
							}
						});

						ed.on('remove', function () {
							element.remove();
						});

						if (uiTinymceConfig.setup) {
							uiTinymceConfig.setup(ed, {
								updateView: updateView
							});
						}

						if (expression.setup) {
							expression.setup(ed, {
								updateView: updateView
							});
						}
					},
					format: expression.format || 'html',
					selector: '#' + attrs.id
				};
				// extend options with initial uiTinymceConfig and
				// options from directive attribute value
				angular.extend(options, uiTinymceConfig, expression, setupOptions);
				// Wrapped in $timeout due to $tinymce:refresh implementation, requires
				// element to be present in DOM before instantiating editor when
				// re-rendering directive
				$timeout(function () {
					if (options.baseURL) {
						tinymce.baseURL = options.baseURL;
					}
					var maybeInitPromise = tinymce.init(options);
					if (maybeInitPromise && typeof maybeInitPromise.then === 'function') {
						maybeInitPromise.then(function () {
							toggleDisable(scope.$eval(attrs.ngDisabled));
						});
					} else {
						toggleDisable(scope.$eval(attrs.ngDisabled));
					}
				});

				ngModel.$formatters.unshift(function (modelValue) {
					return modelValue ? $sce.trustAsHtml(modelValue) : '';
				});

				ngModel.$parsers.unshift(function (viewValue) {
					return viewValue ? $sce.getTrustedHtml(viewValue) : '';
				});

				ngModel.$render = function () {
					ensureInstance();
					var viewValue = ngModel.$viewValue ?
						$sce.getTrustedHtml(ngModel.$viewValue) : '';

					// instance.getDoc() check is a guard against null value
					// when destruction & recreation of instances happen
					if (tinyInstance &&
						tinyInstance.getDoc()
					) {
						tinyInstance.setContent(viewValue);
						// Triggering change event due to TinyMCE not firing event &
						// becoming out of sync for change callbacks
						tinyInstance.fire('change');
					}
				};

				attrs.$observe('disabled', toggleDisable);

				// This block is because of TinyMCE not playing well with removal and
				// recreation of instances, requiring instances to have different
				// selectors in order to render new instances properly
				scope.$on('$tinymce:refresh', function (e, id) {
					var eid = attrs.id;
					if (angular.isUndefined(id) || id === eid) {
						var parentElement = element.parent();
						var clonedElement = element.clone();
						clonedElement.removeAttr('id');
						clonedElement.removeAttr('style');
						clonedElement.removeAttr('aria-hidden');
						tinymce.execCommand('mceRemoveEditor', false, eid);
						parentElement.append($compile(clonedElement)(scope));
					}
				});

				scope.$on('$destroy', function () {
					ensureInstance();

					if (tinyInstance) {
						tinyInstance.remove();
						tinyInstance = null;
					}
				});

				function ensureInstance() {
					if (!tinyInstance) {
						tinyInstance = tinymce.get(attrs.id);
					}
				}
			}
		};
	}]);
