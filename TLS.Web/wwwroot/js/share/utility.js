const base64ToFile = (base64, fileName) => {
    var arr = base64.split(','),
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]),
        n = bstr.length,
        u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], fileName, { type: mime });
}

const fileToBase64 = (fileData) => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(fileData);
    reader.onload = () => resolve(reader.result);
    reader.onerror = () => reject(reader.error);
});

const getQueryStringValue = function (name) {
    const params = new URLSearchParams(window.location.search);
    if (params.has(name)) {
        return params.get(name);
    } else {
        return null;
    }
}