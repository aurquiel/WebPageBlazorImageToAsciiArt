downloadFile = (fileName, data) => {
    const blob = new Blob([data], { type: "text/plain" });

    // Crea un enlace de descarga
    const downloadLink = document.createElement("a");
    downloadLink.href = window.URL.createObjectURL(blob);
    downloadLink.download = fileName;

    // Simula un clic en el enlace para descargar el archivo
    document.body.appendChild(downloadLink);
    downloadLink.click();
    document.body.removeChild(downloadLink);
};