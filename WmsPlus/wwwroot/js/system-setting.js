// ===== 系统设定页面 - JS Interop 函数 =====

// 滚动到指定 section
window.scrollToElement = function (containerElement, sectionId) {
    if (!containerElement) return;
    var section = containerElement.querySelector('#' + sectionId);
    if (section) {
        section.scrollIntoView({ behavior: 'smooth', block: 'start' });
    }
};

// 初始化滚动监听（Scroll Spy）
var _scrollSpyHandler = null;
var _scrollContainer = null;
var _dotNetRef = null;
var _navIds = [];
var _scrollRafId = null;

window.initSystemSettingScrollSpy = function (container, dotNetRef, navIds) {
    _scrollContainer = container;
    _dotNetRef = dotNetRef;
    _navIds = navIds || [];

    if (_scrollSpyHandler) {
        _scrollContainer.removeEventListener('scroll', _scrollSpyHandler);
    }

    _scrollSpyHandler = function () {
        if (_scrollRafId) cancelAnimationFrame(_scrollRafId);
        _scrollRafId = requestAnimationFrame(function () {
            detectAndNotify();
        });
    };

    _scrollContainer.addEventListener('scroll', _scrollSpyHandler, { passive: true });
};

// 检测当前可见的 section 并通知 Blazor
function detectAndNotify() {
    if (!_scrollContainer || !_navIds.length || !_dotNetRef) return;

    var activeNavId = null;
    var minTop = Infinity;

    for (var i = 0; i < _navIds.length; i++) {
        var section = _scrollContainer.querySelector('#section-' + _navIds[i]);
        if (!section) continue;

        var rect = section.getBoundingClientRect();
        // 取在可视区上方偏移量最小（最靠近顶部）的 section
        var topOffset = Math.abs(rect.top);

        // 如果 section 的顶部已经滚过可视区顶部不超过一半高度，则认为它是最活跃的
        if (rect.top <= 100 && topOffset < minTop) {
            minTop = topOffset;
            activeNavId = _navIds[i];
        }
    }

    // 如果所有 section 都没匹配（如滚动到底部），取最后一个
    if (!activeNavId && _navIds.length > 0) {
        activeNavId = _navIds[_navIds.length - 1];
    }

    if (activeNavId) {
        try {
            _dotNetRef.invokeMethodAsync('OnActiveNavChanged', activeNavId);
        } catch (e) {
            console.warn('SystemSetting scroll spy callback error:', e);
        }
    }
}

// 销毁滚动监听
window.destroySystemSettingScrollSpy = function () {
    if (_scrollContainer && _scrollSpyHandler) {
        _scrollContainer.removeEventListener('scroll', _scrollSpyHandler);
    }
    _scrollSpyHandler = null;
    _scrollContainer = null;
    _dotNetRef = null;
    _navIds = [];
    if (_scrollRafId) cancelAnimationFrame(_scrollRafId);
};

// 降级方案：C# 侧主动调用检测当前活跃 section
window.detectActiveSection = function (container, navIds) {
    if (!container || !navIds || !navIds.length) return '';

    var activeNavId = null;
    var minTop = Infinity;

    for (var i = 0; i < navIds.length; i++) {
        var section = container.querySelector('#section-' + navIds[i]);
        if (!section) continue;

        var rect = section.getBoundingClientRect();
        var topOffset = Math.abs(rect.top);

        if (rect.top <= 100 && topOffset < minTop) {
            minTop = topOffset;
            activeNavId = navIds[i];
        }
    }

    if (!activeNavId && navIds.length > 0) {
        activeNavId = navIds[navIds.length - 1];
    }

    return activeNavId || '';
};
