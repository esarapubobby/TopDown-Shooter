mergeInto(LibraryManager.library, {
    IsMobileBrowser: function()
    {
        var ua = navigator.userAgent || navigator.vendor || window.opera;

        return /android|iphone|ipad|ipod|mobile/i.test(ua) ? 1 : 0;
    }
});