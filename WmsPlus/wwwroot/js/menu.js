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
