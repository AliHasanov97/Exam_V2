﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Result.aspx.vb" Inherits="new.az_ex.shop.Result" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="A fully featured admin theme which can be used to build CRM, CMS, etc." />
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

    <link href="css/all.css" rel="stylesheet" />
    <script src="js/all.js"></script>

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
    <link rel="shortcut icon" href="https://yevgenysim-turkey.github.io/dashbrd/assets/favicon/favicon.ico" type="image/x-icon" />

    <!-- Fonts and icons -->
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Inter:ital,opsz,wght@0,14..32,100..900;1,14..32,100..900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@24,400,1,0" />

    <!-- Libs CSS -->
    <link rel="stylesheet" href="https://yevgenysim-turkey.github.io/dashbrd/assets/css/libs.bundle.css" />

    <!-- Theme CSS -->
    <link rel="stylesheet" href="https://yevgenysim-turkey.github.io/dashbrd/assets/css/theme.bundle.css" />

   
    <!-- Title -->
    <title>Dashbrd</title>
</head>

<body>
    <form id="form1" runat="server">
        <!-- Page content -->


        <div class="container p-2">
            <div class="card card-body bg-body-tertiary border-transparent m-1">
                <div class="row align-items-center">
                    <div class="col-auto">
                        <i class="fa-solid fa-circle-info" style="font-size: 22px"></i>
                    </div>
                    <div class="col">
                        <span style="font-size: 18px">İmtahan nəticəsi</span>
                    </div>
                </div>
            </div>
            <div class="row mb-1 d-flex align-items-center">
                <div class="col-12 col-lg-3">
                    <div class="card bg-body-tertiary border-transparent m-1">
                        <div class="card-body">

                            <p class="text-muted m-1">
                                <strong>Tarix :</strong> <span class="ms-2">
                                    <asp:Label ID="Tarix" runat="server" Text="Label"></asp:Label></span>
                            </p>

                            <p class="text-muted m-1">
                                <strong>Fənn :</strong> <span class="ms-2">
                                    <asp:Label ID="Fen" runat="server" Text="Label"></asp:Label></span>
                            </p>

                            <p class="text-muted m-1">
                                <strong>Aralıq :</strong> <span class="ms-2">
                                    <asp:Label ID="Araliq" runat="server" Text="Label"></asp:Label></span>
                            </p>

                        </div>
                    </div>
                </div>
                <div class="col-12 col-lg-3">
                    <div class="card bg-body-tertiary border-transparent m-1">
                        <div class="card-body" style="align-items: center; justify-content: center; display: flex; flex-direction: column;">
                            <span class="text-muted">Düzgün</span>
                            <span id="Trues" runat="server" style="font-size: 40px;">56</span>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-lg-3">
                    <div class="card bg-body-tertiary border-transparent m-1">
                        <div class="card-body" style="align-items: center; justify-content: center; display: flex; flex-direction: column;">
                            <span class="text-muted">Yalnış</span>
                            <span id="Wrong" runat="server" style="font-size: 40px;">56</span>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-lg-3">
                    <div class="card bg-body-tertiary border-transparent m-1">
                        <div class="card-body" style="align-items: center; justify-content: center; display: flex; flex-direction: column;">
                            <span class="text-muted">Cavabsız</span>
                            <span id="Unans" runat="server" style="font-size: 40px;">56</span>
                        </div>
                    </div>
                </div>
                <!-- Header -->
            </div>
            <div class="row mb-1 d-flex">

                <div class="col-12 col-lg-3">
                    <div class="accordion m-1" id="accordionExample">
                        <div class="accordion-item">
                            <h2 class="accordion-header">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                    <i class="fa-solid fa-list" style="padding-right: 10px"></i>Suallarınız
                                </button>
                            </h2>
                            <div id="collapseOne" class="accordion-collapse collapse" data-bs-parent="#accordionExample" style="">
                                <div class="accordion-body" style="height:350px;overflow:hidden;overflow-y:auto">
                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-lg-9">
                    <div class="card bg-body-tertiary border-transparent m-1">
                        <div class="card-body row" id="check" style="">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Accordion -->



            <!-- Divider -->
            <hr class="my-8">
        </div>
    </form>
    <!-- JAVASCRIPT -->
    <!-- Map JS -->

    <script>
        function preview(a) {
            var no = $("#" + a).attr("data-id")
            var ans = $("#" + a).attr("data-Yans")
            $.ajax({
                type: "POST",
                url: "/Result.aspx/Preview",
                data: '{ID: "' + no + '",ans : "' + ans +'"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $("#check").html(response.d)
                }
            })
        }
    </script>

    <!-- Vendor JS -->
    <script src="https://yevgenysim-turkey.github.io/dashbrd/assets/js/vendor.bundle.js"></script>

    <!-- Theme JS -->
    <script src="https://yevgenysim-turkey.github.io/dashbrd/assets/js/theme.bundle.js"></script>
</body>
</html>
