<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="read.aspx.vb" Inherits="new.az_ex.shop.read" %>


<html lang="en" data-bs-theme="dark">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="A fully featured admin theme which can be used to build CRM, CMS, etc.">

    <!-- Light/dark mode -->
    <script>
        /*!
         * Color mode toggler for Bootstrap's docs (https://getbootstrap.com/)
         * Copyright 2011-2024 The Bootstrap Authors
         * Licensed under the Creative Commons Attribution 3.0 Unported License.
         * Modified by Simpleqode
         */

        (() => {
            'use strict';

            const getStoredTheme = () => localStorage.getItem('theme');
            const setStoredTheme = (theme) => localStorage.setItem('theme', theme);

            const getPreferredTheme = () => {
                const storedTheme = getStoredTheme();
                if (storedTheme) {
                    return storedTheme;
                }

                return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
            };

            const setTheme = (theme) => {
                if (theme === 'auto') {
                    document.documentElement.setAttribute('data-bs-theme', window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
                } else {
                    document.documentElement.setAttribute('data-bs-theme', theme);
                }
            };

            setTheme(getPreferredTheme());

            const showActiveTheme = (theme, focus = false) => {
                const themeSwitchers = document.querySelectorAll('[data-bs-theme-switcher]');

                themeSwitchers.forEach((themeSwitcher) => {
                    const themeSwitcherIcon = themeSwitcher.querySelector('.material-symbols-outlined');
                    themeSwitcherIcon.innerHTML = theme === 'light' ? 'light_mode' : theme === 'dark' ? 'dark_mode' : 'contrast';

                    if (focus) {
                        themeSwitcher.focus();
                    }
                });

                document.querySelectorAll('[data-bs-theme-value]').forEach((element) => {
                    element.classList.remove('active');
                    element.setAttribute('aria-pressed', 'false');

                    if (element.getAttribute('data-bs-theme-value') === theme) {
                        element.classList.add('active');
                        element.setAttribute('aria-pressed', 'true');
                    }
                });
            };

            const refreshCharts = () => {
                const charts = document.querySelectorAll('.chart-canvas');

                charts.forEach((chart) => {
                    const chartId = chart.getAttribute('id');
                    const instance = Chart.getChart(chartId);

                    if (!instance) {
                        return;
                    }

                    if (instance.options.scales.y) {
                        instance.options.scales.y.grid.color = getComputedStyle(document.documentElement).getPropertyValue('--bs-border-color');
                        instance.options.scales.y.ticks.color = getComputedStyle(document.documentElement).getPropertyValue('--bs-secondary-color');
                    }

                    if (instance.options.scales.x) {
                        instance.options.scales.x.ticks.color = getComputedStyle(document.documentElement).getPropertyValue('--bs-secondary-color');
                    }

                    if (instance.options.elements.arc) {
                        instance.options.elements.arc.borderColor = getComputedStyle(document.documentElement).getPropertyValue('--bs-body-bg');
                        instance.options.elements.arc.hoverBorderColor = getComputedStyle(document.documentElement).getPropertyValue('--bs-body-bg');
                    }

                    instance.update();
                });
            };

            window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
                const storedTheme = getStoredTheme();
                if (storedTheme !== 'light' && storedTheme !== 'dark') {
                    setTheme(getPreferredTheme());
                }
            });

            window.addEventListener('DOMContentLoaded', () => {
                showActiveTheme(getPreferredTheme());

                document.querySelectorAll('[data-bs-theme-value]').forEach((toggle) => {
                    toggle.addEventListener('click', (e) => {
                        e.preventDefault();
                        const theme = toggle.getAttribute('data-bs-theme-value');
                        setStoredTheme(theme);
                        setTheme(theme);
                        showActiveTheme(theme, true);
                        refreshCharts();
                    });
                });
            });
        })();
    </script>

    <!-- Favicon -->
    <link rel="shortcut icon" href="https://yevgenysim-turkey.github.io/dashbrd/assets/favicon/favicon.ico" type="image/x-icon">

    <!-- Fonts and icons -->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="">
    <link href="https://fonts.googleapis.com/css2?family=Inter:ital,opsz,wght@0,14..32,100..900;1,14..32,100..900&amp;display=swap" rel="stylesheet">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,1,0">

    <!-- Libs CSS -->
    <link rel="stylesheet" href="https://yevgenysim-turkey.github.io/dashbrd/assets/css/libs.bundle.css">

    <!-- Theme CSS -->
    <link rel="stylesheet" href="https://yevgenysim-turkey.github.io/dashbrd/assets/css/theme.bundle.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.0/css/all.min.css" integrity="sha512-9xKTRVabjVeZmc+GUW8GgSmcREDunMM+Dt/GrzchfN8tkwHizc5RP4Ok/MXFFy5rIjJjzhndFScTceq5e6GvVQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <script src="js/Jquery.js"></script>
    <link
        rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
    <!-- Title -->
    <title>Dashbrd</title>
</head>

<body style="width: auto">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- Sidenav -->
        <!-- Sidenav (sm) -->
        <aside class="aside aside-sm d-none d-xl-flex">
            <nav class="navbar navbar-expand-xl navbar-vertical">
                <div class="container-fluid">
                    <div class="collapse navbar-collapse" id="sidenavSmallCollapse">
                        <!-- Nav -->
                        <nav class="navbar-nav nav-pills h-100">
                            <div class="nav-item">
                                <div data-bs-toggle="tooltip" data-bs-placement="right" data-bs-trigger="hover" data-bs-title="Color mode">
                                    <a class="nav-link" data-bs-toggle="collapse" data-bs-theme-switcher="" href="#colorModeOptions" role="button" aria-expanded="false" aria-controls="colorModeOptions">
                                        <span class="material-symbols-outlined mx-auto">dark_mode</span>
                                    </a>
                                </div>
                                <div class="collapse" id="colorModeOptions">
                                    <div class="border-top border-bottom py-2">
                                        <a class="nav-link fs-sm" data-bs-toggle="tooltip" data-bs-placement="right" data-bs-trigger="hover" data-bs-title="Light" data-bs-theme-value="light" href="#" role="button" aria-pressed="false">
                                            <span class="material-symbols-outlined mx-auto">light_mode </span>
                                        </a>
                                        <a class="nav-link fs-sm active" data-bs-toggle="tooltip" data-bs-placement="right" data-bs-trigger="hover" data-bs-title="Dark" data-bs-theme-value="dark" href="#" role="button" aria-pressed="true">
                                            <span class="material-symbols-outlined mx-auto">dark_mode </span>
                                        </a>
                                        <a class="nav-link fs-sm" data-bs-toggle="tooltip" data-bs-placement="right" data-bs-trigger="hover" data-bs-title="Auto" data-bs-theme-value="auto" href="#" role="button" aria-pressed="false">
                                            <span class="material-symbols-outlined mx-auto">contrast </span>
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div class="nav-item" data-bs-toggle="tooltip" data-bs-placement="right" data-bs-title="Go to product page">
                                <a class="nav-link" href="https://themes.getbootstrap.com/product/dashbrd/" target="_blank">
                                    <span class="material-symbols-outlined mx-auto">local_mall </span>
                                </a>
                            </div>
                            <div class="nav-item mt-auto" data-bs-toggle="tooltip" data-bs-placement="right" data-bs-title="Contact us">
                                <a class="nav-link" href="mailto:yevgenysim+simpleqode@gmail.com">
                                    <span class="material-symbols-outlined mx-auto">support </span>
                                </a>
                            </div>
                        </nav>
                    </div>
                </div>
            </nav>
        </aside>

        <!-- Sidenav (lg) -->
        <aside class="aside">
            <nav class="navbar navbar-expand-xl navbar-vertical">
                <div class="container-fluid">
                    <!-- Brand -->
                    <a class="navbar-brand fs-5 fw-bold px-xl-3 mb-xl-4" href="./index.html">
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" class="fs-4 text-secondary me-1 duo-icon duo-icon-box-2" data-duoicon="box-2">
                            <path fill="currentColor" d="m20.765 7.982.022.19.007.194v7.268a2.5 2.5 0 0 1-1.099 2.07l-.15.095-6.295 3.634-.124.067-.126.06v-8.99l7.765-4.588Z" class="duoicon-secondary-layer" opacity=".3"></path><path fill="currentColor" d="m13.25 2.567 6.294 3.634c.05.03.1.06.148.092L12 10.838 4.308 6.293a2.81 2.81 0 0 1 .148-.092l6.294-3.634a2.498 2.498 0 0 1 2.5 0Z" class="duoicon-primary-layer"></path><path fill="currentColor" d="M3.235 7.982 11 12.571v8.988a2.339 2.339 0 0 1-.25-.126l-6.294-3.634a2.502 2.502 0 0 1-1.25-2.165V8.366c0-.13.01-.258.03-.384h-.001Z" class="duoicon-secondary-layer" opacity=".3"></path></svg>
                        Dashbrd
                          </a>

                    <!-- User -->
                    <div class="ms-auto d-xl-none">
                        <div class="dropdown my-n2 row">
                            <div id="List2" runat="server" class="notification-list col-2" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                <a class="nav-link" href="javascript: void(0);">
                                    <i class="fas fa-clipboard-list" style="font-size: 22px; vertical-align: middle; line-height: 70px;"></i>
                                </a>
                            </div>
                            <a class="col btn btn-link d-inline-flex align-items-center dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="avatar avatar-sm avatar-status avatar-status-success me-3">
                                    <img id="Avatar" runat="server" class="avatar-img" src="./assets/img/photos/photo-6.jpg" alt="..." />
                                </span>
                                <span id="Name" runat="server"></span>
                            </a>
                            <ul class="dropdown-menu dropdown-menu-end">
                                <li><a class="dropdown-item" href="#">Account</a></li>
                                <li><a class="dropdown-item" href="#">Change password</a></li>
                                <li>
                                    <hr class="dropdown-divider">
                                </li>
                                <li><a class="dropdown-item" href="#">Sign out</a></li>
                            </ul>
                        </div>
                    </div>

                    <!-- Toggler -->
                    <button class="navbar-toggler ms-3" type="button" data-bs-toggle="collapse" data-bs-target="#sidenavLargeCollapse" aria-controls="sidenavLargeCollapse" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>

                    <!-- Collapse -->
                    <div class="collapse navbar-collapse" id="sidenavLargeCollapse">
                        <!-- Search -->
                        <div class="input-group d-xl-none my-4 my-xl-0">
                            <input class="form-control" id="topnavSearchInputMobile" type="search" placeholder="Search" aria-label="Search" aria-describedby="navbarSearchMobile">
                            <span class="input-group-text" id="navbarSearchMobile">
                                <span class="material-symbols-outlined">search </span>
                            </span>
                        </div>

                        <!-- Nav -->


                        <!-- Heading -->

                        <!-- Nav -->
                        <!--Plugin CSS file with desired skin-->
                        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ion-rangeslider/2.3.1/css/ion.rangeSlider.min.css" />

                        <!--jQuery-->
                        <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

                        <!--Plugin JavaScript file-->
                        <script src="https://cdnjs.cloudflare.com/ajax/libs/ion-rangeslider/2.3.1/js/ion.rangeSlider.min.js"></script>

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="card mt-auto mb-7 card-line" id="panel1" runat="server" visible="false">
                                    <div class="card-body">
                                        <!-- Heading -->
                                        <h6>Status</h6>

                                        <!-- Text -->
                                        <div class="progress">
                                            <div id="p_true" class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style="width: 0%" aria-valuenow="15" aria-valuemin="0" aria-valuemax="100"></div>
                                            <div id="p_wrong" class="progress-bar bg-success progress-bar-striped progress-bar-animated" role="progressbar" style="width: 0%" aria-valuenow="30" aria-valuemin="0" aria-valuemax="100"></div>
                                        </div>
                                        <br />
                                        <center>
                                            <p>Düzgün cavab sayı : <span id="true">0</span></p>
                                            <p>Yanlış cavab sayı : <span id="wrong">0</span></p>
                                            <p>Cavabsız sual sayı : <span id="unans" runat="server">0</span></p>
                                        </center>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="card border-transparent mb-7 row" style="padding: 10px; margin: 2px;">
                            <center>
                                <asp:Label ID="Subject" runat="server" Text="Label"></asp:Label>
                                <asp:Label ID="Max" runat="server" Text="0"></asp:Label>
                                <div class="range-slider">
                                    <input type="text" class="js-range-slider" value="" />
                                </div>
                                <div class="row col-11" style="margin-top: 10px;">
                                    <input type="text" id="S_min" runat="server" class="js-input-from col form-control text-center" style="margin-bottom: 10px">
                                    <input type="text" id="S_max" runat="server" class="js-input-to col form-control text-center" style="margin-bottom: 10px">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="true">
                                        <ContentTemplate>
                                            <asp:Button ID="Button1" CssClass="btn btn-primary form-control col-12" runat="server" Text="Sualları oxu" />
                                            <asp:Button ID="Button2" CssClass="btn btn-secondary form-control col-12" runat="server" Text="Mini imtahan" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </center>
                        </div>




                        <script>
                            var $range = $(".js-range-slider"),
                                $inputFrom = $(".js-input-from"),
                                $inputTo = $(".js-input-to"),
                                instance,
                                min = 1,
                                max = $("#Max").text(),
                                from = 1,
                                to = 10;

                            $range.ionRangeSlider({
                                skin: "flat",
                                type: "double",
                                min: min,
                                max: max,
                                from: 1,
                                to: 10,
                                onStart: updateInputs,
                                onChange: updateInputs
                            });
                            instance = $range.data("ionRangeSlider");

                            function updateInputs(data) {
                                from = data.from;
                                to = data.to;

                                $inputFrom.prop("value", from);
                                $inputTo.prop("value", to);
                            }

                            $inputFrom.on("input", function () {
                                var val = $(this).prop("value");

                                // validate
                                if (val < min) {
                                    val = min;
                                } else if (val > to) {
                                    val = to;
                                }

                                instance.update({
                                    from: val
                                });
                            });

                            $inputTo.on("input", function () {
                                var val = $(this).prop("value");

                                // validate
                                if (val < from) {
                                    val = from;
                                } else if (val > max) {
                                    val = max;
                                }

                                instance.update({
                                    to: val
                                });
                            });
                        </script>


                        <!-- Divider -->
                        <hr class="my-4 d-xl-none">

                        <!-- Nav -->
                        <nav class="navbar-nav nav-pills d-xl-none mb-7">
                            <div class="nav-item">
                                <a class="nav-link" data-bs-toggle="collapse" data-bs-theme-switcher="" href="#colorModeOptionsMobile" role="button" aria-expanded="false" aria-controls="colorModeOptionsMobile">
                                    <span class="material-symbols-outlined me-3">dark_mode</span> Color mode
                </a>
                                <div class="collapse" id="colorModeOptionsMobile">
                                    <div class="nav nav-pills">
                                        <a class="nav-link" data-bs-theme-value="light" href="#" role="button" aria-pressed="false">Light </a>
                                        <a class="nav-link active" data-bs-theme-value="dark" href="#" role="button" aria-pressed="true">Dark </a>
                                        <a class="nav-link" data-bs-title="Auto" data-bs-theme-value="auto" href="#" role="button" aria-pressed="false">Auto </a>
                                    </div>
                                </div>
                            </div>
                            <div class="nav-item">
                                <a class="nav-link" href="https://themes.getbootstrap.com/product/dashbrd/" target="_blank">
                                    <span class="material-symbols-outlined me-3">local_mall</span> Go to product page
                </a>
                            </div>
                            <div class="nav-item">
                                <a class="nav-link" href="mailto:yevgenysim+simpleqode@gmail.com">
                                    <span class="material-symbols-outlined me-3">alternate_email</span> Contact us
                </a>
                            </div>
                        </nav>

                        <!-- Card -->

                    </div>
                </div>
            </nav>
        </aside>

        <!-- Topnav -->
        <nav class="navbar d-none d-xl-flex px-xl-6">
            <div class="container flex-column align-items-stretch">
                <div class="row">
                    <div class="col">
                        <!-- Search -->
                        <div class="input-group" style="max-width: 400px">
                            <input class="form-control" id="topnavSearchInput" type="search" placeholder="Search" aria-label="Search" aria-describedby="navbarSearch">
                            <span class="input-group-text" id="navbarSearch">
                                <kbd class="badge bg-body-secondary text-body">⌘</kbd>
                                <kbd class="badge bg-body-secondary text-body ms-1">K</kbd>
                            </span>
                        </div>
                    </div>
                    <div class="col-auto">
                        <!-- User -->
                        <div class="dropdown my-n2 row">
                            <div id="List1" runat="server" class="notification-list col-2" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                        <a class="nav-link" href="javascript: void(0);">
                                            <i class="fas fa-clipboard-list" style="font-size: 22px; vertical-align: middle; line-height: 70px;"></i>
                                        </a>
                                    </div>
                            <a class="col btn btn-link d-inline-flex align-items-center dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                <span class="avatar avatar-sm avatar-status avatar-status-success me-3">
                                    <img id="Avatar1" runat="server" class="avatar-img" src="./assets/img/photos/photo-6.jpg" alt="..." />
                                </span>
                                <span id="Name1" runat="server"></span>
                            </a>
                            <ul class="dropdown-menu dropdown-menu-end">
                                <li><a class="dropdown-item" href="#">Account</a></li>
                                <li><a class="dropdown-item" href="#">Change password</a></li>
                                <li>
                                    <hr class="dropdown-divider">
                                </li>
                                <li><a class="dropdown-item" href="#">Sign out</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </nav>
        <!-- Main -->
        <main class="main px-lg-6">
            <!-- Content -->
            <div class="container-lg">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        <span id="tools" style="visibility: hidden; position: absolute; background-color: black; color: white; border-radius: 25%; font-size: 22px; padding: 10px">-</span>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <hr class="my-8">
            </div>



            <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                    </div>
                </div>
            </div>












        </main>

        <!-- JAVASCRIPT -->
        <!-- Map JS -->
        <script src="https://api.mapbox.com/mapbox-gl-js/v0.53.0/mapbox-gl.js"></script>

        <!-- Vendor JS -->
        <script src="https://yevgenysim-turkey.github.io/dashbrd/assets/js/vendor.bundle.js"></script>

        <!-- Theme JS -->
        <script src="https://yevgenysim-turkey.github.io/dashbrd/assets/js/theme.bundle.js"></script>
        <script>

            function get(x) {
                var trues = parseInt($("#true").text())
                var wrong = parseInt($("#wrong").text())
                var unans = parseInt($("#unans").text())
                var ans = $("input:radio[name=" + x + "]:checked").val()
                var id = $(".num" + x).text()
                $.ajax({
                    type: "POST",
                    url: "/read.aspx/Query",
                    data: '{ID : "' + id + '",ans: "' + ans + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d != '2') {
                            if (response.d != '1') {
                                document.documentElement.style.setProperty('--animate-duration', '1s');
                                $("#" + x + "pan").html(response.d)
                                document.getElementById(x + 'pans').classList.add('animate__animated', 'animate__pulse');
                                //$("#" + x + "pans").addClass('animate__animated', 'animate__bounceOutLeft')
                                wrong += 1
                                unans -= 1
                                $("#wrong").text(wrong)
                                $("#unans").text(unans)
                                var p_value = (wrong / (trues + wrong + unans)) * 100
                                var title = $('#p_wrong').css("width", p_value + '%')

                            } else {
                                document.documentElement.style.setProperty('--animate-duration', '5s');
                                $("#" + x + "pan").html("<p><i class='fa-solid fa-check'></i> Cavabınız doğrudur</p>")
                                document.getElementById(x + 'pans').classList.add('animate__animated', 'animate__bounceOutLeft');
                                setTimeout(function () {
                                    $("#" + x + "pans").remove();
                                }, 3000);
                                trues += 1
                                unans -= 1
                                $("#unans").text(unans)
                                $("#true").text(trues)
                                var p_value = (trues / (trues + wrong + unans)) * 100
                                var title = $('#p_true').css("width", p_value + '%')
                            }
                        }
                    }
                }
                );
            }
            function Report(s, i) {
                $.ajax({
                    type: "POST",
                    url: "/read.aspx/Report",
                    data: '{ID : "' + s + '",que: "' + i + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $(".modal-content").html(response.d)
                    }
                })
            }
            function Offer(a, b) {
                var c = $('#DropDownList1').val()
                var d = $('#floatingTextarea').val()
                $.ajax({
                    type: "POST",
                    url: "/read.aspx/Offer",
                    data: '{subs: "' + a + '",num: "' + b + '",ans: "' + c + '",note: "' + d + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $(".modal-content").html(response.d)
                    }
                })
            }
            function Requests(a) {
                $.ajax({
                    type: "POST",
                    url: "/read.aspx/Requests",
                    data: '{subs: "' + a + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $(".modal-content").html(response.d)
                    }
                })
            }
            function Del(a,b) {
                $.ajax({
                    type: "POST",
                    url: "/read.aspx/Del",
                    data: '{ID: "' + a + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        Requests(b)
                    }
                })
            }
            function Bax(a) {
                $.ajax({
                    type: "POST",
                    url: "/read.aspx/Bax",
                    data: '{ID: "' + a + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        $(".modal-content").html(response.d)
                    }
                })
            }
            function Answer(x) {
                $("#tools").text(x.getAttribute("data-ans"))
                document.getElementById("tools").style.visibility = "visible";
                document.getElementById("tools").style.top = event.pageY + 15;
                document.getElementById("tools").style.left = event.pageX + 15;
            }
            function Leave() {
                document.getElementById("tools").style.visibility = "hidden";
            }

        </script>
    </form>
</body>
</html>
