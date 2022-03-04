"use strict";
var substringMenuLv1 = "000",
    substringMenuLv2 = "000",
    substringMenuLv3 = "000",
    MenuLevel_1 = [],
    MenuLevel_2 = [],
    MenuLevel_3 = [],
    MenuLevel_4 = [],
    SessionData = undefined,
    ssd = undefined;
$(document).ready(function () {

    //const notifications = $.connection.cusHub;

    //notifications.client.ControlUserLog = () => {
    //    ControlUserLog();
    //};
    //$.connection.hub.start().done(() => {
    //    ControlUserLog();
    //});

    //async function ControlUserLog() {
    //    $.ajax({
    //        url: "/Login/ControlUserLog",
    //    }).done((response) => {
    //        if (response.flag) {
    //            Logout();
    //        }
    //    });
    //}

    async function Logout() {
        $.ajax({
            url: "/Login/Logout",
        });
    }

    function t(t) {
        var rt = undefined,
            ut = !0,
            dt = !1,
            gt = undefined,
            s, ni, et, f, e, h, ri, v, st, ht, y, ui, fi, p, ct, lt, w, ei, oi, b, ci, u, c, l, a, k, vt, yt, d, li, ai, g, pt, wt, nt, vi, yi, tt, bt, kt, it, pi, wi;
        try {
            for (s = t[Symbol.iterator](); !(ut = (ni = s.next()).done); ut = !0)
                if (u = ni.value, u.MenuLevel == 2) {
                    rt = u.MenuOrderly;
                    break;
                }
        } catch (ft) {
            dt = !0;
            gt = ft;
        } finally {
            try {
                !ut && s["return"] && s["return"]();
            } finally {
                if (dt) throw gt;
            }
        }
        et = 0;
        t.forEach(function (n) {
            var t = undefined,
                i, r;
            t = n.MenuOrderly;
            t > et && t < 1e3 && (et = t, i = n.MenuOrderly, MenuLevel_1.push(i), r = '<a class="nav-item menubar  menu-detail" data-focus="' + n.MultiLang + '" data-levelmenu_1="' + i + '" data-Code="' + n.Code + '">\n                                    ' + n.MultiLang + '\n<span class="menubar menu-init" data-subfocus="' + n.MultiLang + '" ><\/span>\n                            <\/a>', $(".topnav").append(r));
        });
        f = function (i) {
            $(".side-menu-item").empty();
            var r = 0;
            MenuLevel_2 = [];
            t.forEach(function (t) {
                var f = t.MenuLevel2Orderly,
                    u;
                t.MenuOrderly === i && f > r && (r = f, u = t.MenuLevel2Orderly, MenuLevel_2.push(u), $(".side-menu-item").append('<li class="nav-item has-treeview submenu-detail" data-levelmenu_2="' + u + '" data-Code="' + t.Code + '">\n\n<a href = "#" class="nav-link collapsed" data-toggle="collapse" data-target="#collapse_' + u + '" aria-expanded="false" aria-controls="collapse_' + u + '">\n\n<i class="fas ' + n[r - 1] + '"><\/i>\n<span class="text-black"  data-level2="' + t.Code + '" style="padding-left:3px">' + t.MultiLang + '<\/span>\n<\/a>\n\n<\/li>\n<hr class= "sidebar-divider"/>'));
            });
        };
        e = function (n) {
            for (var i, u = MenuLevel_2.length, f = function (r) {
                i = 0;
                MenuLevel_3 = [];
                $('[data-levelmenu_2="' + MenuLevel_2[r] + '"]').append('<div id = "collapse_' + MenuLevel_2[r] + '" class= "collapse" aria-labelledby="headingTwo" data-parent="#accordionSidebar">\n<div class="bg-white py-2 collapse-inner rounded">\n<p><\/p>');
                t.forEach(function (t) {
                    var u = t.MenuLevel3Orderly,
                        f;
                    t.MenuOrderly === n && t.MenuLevel2Orderly == MenuLevel_2[r] && u > i && (i = u, f = t.MenuLevel3Orderly, MenuLevel_3.push(f), $('[data-levelmenu_2="' + MenuLevel_2[r] + '"]>div>div').append('<a href="' + t.Link + '" class="collapse-item" data-Code="' + t.Code + ' data-levelmenu_3=" ' + u + '">\n<p class="level3" style="padding-left:7px">' + t.MultiLang + "<\/p>\n<\/a> "));
                });
                i = 0;
            }, r = 0; r < u; r++) f(r);
        };
        f(rt);
        e(rt);
        var o = $("#current_menu").data("level_1"),
            i = $("#current_menu").data("level_2"),
            r = $("#current_menu").data("level_3");
        $("#current_menu").html('<small style="font-size: 14px;"><i class="fa fa-dashboard"><\/i>' + o + "  >  " + i + "  >  " + r + "<\/small>");
        $("div>nav>ul>li>a").has('p:contains("' + i + '")').each(function () {
            $(this).text() === i && $(this).addClass("active");
        });
        $("div>nav>ul>li>ul>li>a").has('p:contains("' + r + '")').each(function () {
            $(this).text() === r && $(this).addClass("active");
        });
        $(".menu-detail").click(function () {
            var n = $(this).data("levelmenu_1"),
                t;
            f(n);
            e(n);
            t = $(this).data("focus");
            $(".menubar").each(function () {
                $(this).data("subfocus") === t && ($(".menubar").removeClass("menu-focus"), $(this).addClass("menu-focus"));
            });
        });
        switch (o) {
            case "MMS":
                var ot = !0,
                    ti = !1,
                    ii = undefined;
                try {
                    for (h = t[Symbol.iterator](); !(ot = (ri = h.next()).done); ot = !0)
                        if (u = ri.value, u.FullName == "MMS") {
                            f(u.MenuOrderly);
                            e(u.MenuOrderly);
                            break;
                        }
                } catch (ft) {
                    ti = !0;
                    ii = ft;
                } finally {
                    try {
                        !ot && h["return"] && h["return"]();
                    } finally {
                        if (ti) throw ii;
                    }
                }
                c = $("div>li>a").has('span:contains("' + i + '")');
                l = c.attr("data-target");
                $("div>li").find("a").has('span:contains("' + i + '")') && jQuery(l).addClass("show");
                $("div.collapse-inner>a.collapse-item").has('p.level3:contains("' + r + '")').each(function () {
                    var n = $(this).text().trim();
                    n === r && $(this).addClass("active");
                });
                a = o;
                $(".menubar").each(function () {
                    $(this).data("subfocus") == a && ($(".menubar").removeClass("menu-focus"), $(this).addClass("menu-focus"));
                });
                break;
            case "WMS":
                if (fi = function () {
                    var n, u, s, h;
                    v = !0;
                    st = !1;
                    ht = undefined;
                    try {
                        for (y = t[Symbol.iterator](); !(v = (ui = y.next()).done); v = !0)
                            if (n = ui.value, n.FullName == "WMS") {
                                f(n.MenuOrderly);
                                e(n.MenuOrderly);
                                break;
                            }
                    } catch (c) {
                        st = !0;
                        ht = c;
                    } finally {
                        try {
                            !v && y["return"] && y["return"]();
                        } finally {
                            if (st) throw ht;
                        }
                    }
                    return u = $("div>li>a").has('span:contains("' + i + '")'), s = u.attr("data-target"), $("div>li").find("a").has('span:contains("' + i + '")') && jQuery(s).addClass("show"), $("div.collapse-inner>a.collapse-item").has('p.level3:contains("' + r + '")').each(function () {
                        var n = $(this).text().trim();
                        n === r && $(this).addClass("active");
                    }), h = o, $(".menubar").each(function () {
                        $(this).data("subfocus") == h && ($(".menubar").removeClass("menu-focus"), $(this).addClass("menu-focus"));
                    }), "break";
                }(), fi === "break") break;
            case "QMS":
                if (oi = function () {
                    var n, u, s, h;
                    p = !0;
                    ct = !1;
                    lt = undefined;
                    try {
                        for (w = t[Symbol.iterator](); !(p = (ei = w.next()).done); p = !0)
                            if (n = ei.value, n.FullName == "QMS") {
                                f(n.MenuOrderly);
                                e(n.MenuOrderly);
                                break;
                            }
                    } catch (c) {
                        ct = !0;
                        lt = c;
                    } finally {
                        try {
                            !p && w["return"] && w["return"]();
                        } finally {
                            if (ct) throw lt;
                        }
                    }
                    return u = $("div>li>a").has('span:contains("' + i + '")'), s = u.attr("data-target"), $("div>li").find("a").has('span:contains("' + i + '")') && jQuery(s).addClass("show"), $("div.collapse-inner>a.collapse-item").has('p.level3:contains("' + r + '")').each(function () {
                        var n = $(this).text().trim();
                        n === r && $(this).addClass("active");
                    }), h = o, $(".menubar").each(function () {
                        $(this).data("subfocus") == h && ($(".menubar").removeClass("menu-focus"), $(this).addClass("menu-focus"));
                    }), "break";
                }(), oi === "break") break;
            case "Monitor":
                var at = !0,
                    si = !1,
                    hi = undefined;
                try {
                    for (b = t[Symbol.iterator](); !(at = (ci = b.next()).done); at = !0)
                        if (u = ci.value, u.FullName == "Monitor") {
                            f(u.MenuOrderly);
                            e(u.MenuOrderly);
                            break;
                        }
                } catch (ft) {
                    si = !0;
                    hi = ft;
                } finally {
                    try {
                        !at && b["return"] && b["return"]();
                    } finally {
                        if (si) throw hi;
                    }
                }
                c = $("div>li>a").has('span:contains("' + i + '")');
                l = c.attr("data-target");
                $("div>li").find("a").has('span:contains("' + i + '")') && jQuery(l).addClass("show");
                $("div.collapse-inner>a.collapse-item").has('p.level3:contains("' + r + '")').each(function () {
                    var n = $(this).text().trim();
                    n === r && $(this).addClass("active");
                });
                a = o;
                $(".menubar").each(function () {
                    $(this).data("subfocus") == a && ($(".menubar").removeClass("menu-focus"), $(this).addClass("menu-focus"));
                });
                break;
            case "Standard":
                if (ai = function () {
                    var n, u, s, h;
                    k = !0;
                    vt = !1;
                    yt = undefined;
                    try {
                        for (d = t[Symbol.iterator](); !(k = (li = d.next()).done); k = !0)
                            if (n = li.value, n.FullName == "Standard") {
                                f(n.MenuOrderly);
                                e(n.MenuOrderly);
                                break;
                            }
                    } catch (c) {
                        vt = !0;
                        yt = c;
                    } finally {
                        try {
                            !k && d["return"] && d["return"]();
                        } finally {
                            if (vt) throw yt;
                        }
                    }
                    return u = $("div>li>a").has('span:contains("' + i + '")'), s = u.attr("data-target"), $("div>li").find("a").has('span:contains("' + i + '")') && jQuery(s).addClass("show"), $("div.collapse-inner>a.collapse-item").has('p.level3:contains("' + r + '")').each(function () {
                        var n = $(this).text().trim();
                        n === r && $(this).addClass("active");
                    }), h = o, $(".menubar").each(function () {
                        $(this).data("subfocus") == h && ($(".menubar").removeClass("menu-focus"), $(this).addClass("menu-focus"));
                    }), "break";
                }(), ai === "break") break;
            case "Cài đặt":
                if (yi = function () {
                    var n, u, s, h;
                    g = !0;
                    pt = !1;
                    wt = undefined;
                    try {
                        for (nt = t[Symbol.iterator](); !(g = (vi = nt.next()).done); g = !0)
                            if (n = vi.value, n.FullName == "Standard") {
                                f(n.MenuOrderly);
                                e(n.MenuOrderly);
                                break;
                            }
                    } catch (c) {
                        pt = !0;
                        wt = c;
                    } finally {
                        try {
                            !g && nt["return"] && nt["return"]();
                        } finally {
                            if (pt) throw wt;
                        }
                    }
                    return u = $("div>li>a").has('span:contains("' + i + '")'), s = u.attr("data-target"), $("div>li").find("a").has('span:contains("' + i + '")') && jQuery(s).addClass("show"), $("div.collapse-inner>a.collapse-item").has('p.level3:contains("' + r + '")').each(function () {
                        var n = $(this).text().trim();
                        n === r && $(this).addClass("active");
                    }), h = o, $(".menubar").each(function () {
                        $(this).data("subfocus") == h && ($(".menubar").removeClass("menu-focus"), $(this).addClass("menu-focus"));
                    }), "break";
                }(), yi === "break") break;
            case "표준":
                if (wi = function () {
                    var n, u, s, h;
                    tt = !0;
                    bt = !1;
                    kt = undefined;
                    try {
                        for (it = t[Symbol.iterator](); !(tt = (pi = it.next()).done); tt = !0)
                            if (n = pi.value, n.FullName == "Standard") {
                                f(n.MenuOrderly);
                                e(n.MenuOrderly);
                                break;
                            }
                    } catch (c) {
                        bt = !0;
                        kt = c;
                    } finally {
                        try {
                            !tt && it["return"] && it["return"]();
                        } finally {
                            if (bt) throw kt;
                        }
                    }
                    return u = $("div>li>a").has('span:contains("' + i + '")'), s = u.attr("data-target"), $("div>li").find("a").has('span:contains("' + i + '")') && jQuery(s).addClass("show"), $("div.collapse-inner>a.collapse-item").has('p.level3:contains("' + r + '")').each(function () {
                        var n = $(this).text().trim();
                        n === r && $(this).addClass("active");
                    }), h = o, $(".menubar").each(function () {
                        $(this).data("subfocus") == h && ($(".menubar").removeClass("menu-focus"), $(this).addClass("menu-focus"));
                    }), "break";
                }(), wi === "break") break;
        }
    }
    var n = ["fa-tachometer-alt", "fa-wrench", "fa-anchor", "fa-cogs", "fa-th", "fa-database", "fa-retweet", "fa-shield", "fa-suitcase", "fa-recycle", "fa-bookmark-o", "fa-bolt", "fa-diamond", "fa-flask"];

    //$.ajax({
    //    url: "/LoginBase/GetSessionData",
    //    type: "GET",
    //    datatype: "json",
    //    data: {},
    //    success: function (n)
    //    {
    //        window.localStorage.setItem("userSession", JSON.stringify(n));
    //        SessionData = n;
    //        t(n.Menus);
    //        document.getElementById("username").innerHTML = n.UserName;
    //    },
    //    error: function () { }
    //});

    ssd = JSON.parse(window.localStorage.getItem(`userSession`));
    if (!ssd) {
        $.ajax({
            url: `/LoginBase/GetSessionData`,
            type: `GET`,
            datatype: `json`,
            data: {},
            //async: false
        })
            .done(function (n) {
                window.localStorage.setItem("userSession", JSON.stringify(n));
                SessionData = n;
                t(n.Menus);
                document.getElementById("username").innerHTML = n.Name;
            });
    }
    else {
        SessionData = ssd;
        t(ssd.Menus);
        document.getElementById("username").innerHTML = ssd.Name;
    }

});
$(document).on("click", "menu-detail", function () {
    document.getElementById("keepMenuCode").value = $(this).data("Code");
});
$(document).on("click", ".submenu-detail", function () {
    document.getElementById("keepMenuCode").value = $(this).data("code");
});
$(document).on("click", ".nav-item", function () {
    document.getElementById("keepMenuCode").value = $(this).data("code");
});
$(document).on("click", ".collapse-item", function () {
    //console.log($(this).data("code"));
    document.getElementById("keepMenuCode").value = $(this).data("code");
});
$(document).on("click", ".has-treeview", function () {
    //console.log($(this).data("Code"));
    document.getElementById("keepMenuCode").value = $(this).data("Code");
});