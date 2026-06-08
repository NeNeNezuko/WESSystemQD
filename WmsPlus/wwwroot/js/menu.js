window.getMenuPosition = function (elementId) {
    const element = document.getElementById(elementId);
    if (element) {
        return element.getBoundingClientRect().top;
    }
    return 0;
};

window.setMegaMenuPosition = function (topPosition) {
    const panel = document.querySelector('.mega-menu-panel');
    if (panel) {
        panel.style.top = topPosition + 'px';
    }
};

window.exportToExcel = function (rows, fileName) {
    var wb = XLSX.utils.book_new();
    var ws = XLSX.utils.aoa_to_sheet(rows);
    XLSX.utils.book_append_sheet(wb, ws, "入库通知单");
    XLSX.writeFile(wb, fileName);
};
