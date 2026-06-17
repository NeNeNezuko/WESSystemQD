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
    XLSX.utils.book_append_book(wb, ws, "入库通知单");
    XLSX.writeFile(wb, fileName);
};

// ★ 标签栏滚动导航
window.scrollTagsContainer = function (element, delta) {
    if (element && element.scrollLeft !== undefined) {
        const newScrollLeft = element.scrollLeft + delta;
        element.scrollTo({ left: newScrollLeft, behavior: 'smooth' });
        return true;
    }
    return false;
};

window.getTagsScrollState = function (element) {
    if (!element || element.scrollWidth === undefined) {
        return [false, false];
    }
    
    // 使用 Math.round 避免浮点数精度问题
    const canLeft = Math.round(element.scrollLeft) > 0;
    const canRight = Math.round(element.scrollLeft) < (element.scrollWidth - element.clientWidth - 1);
    
    return [canLeft, canRight];
};
