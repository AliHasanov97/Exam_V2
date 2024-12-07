<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Cabinet.Master" CodeBehind="tarifs.aspx.vb" Inherits="new.az_ex.shop.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Menu" runat="server">
    <h3 class="fs-base px-3 mb-4">İdarəetmə paneli</h3>

    <!-- Nav -->
    <nav class="navbar-nav nav-pills mb-xl-7">
        <div class="nav-item">
            <a class="nav-link " href="#" data-bs-toggle="collapse" data-bs-target="#customers" rol="button" aria-expanded="false" aria-controls="customers">
                <span class="material-symbols-outlined me-3">group</span> Hesab
                </a>
            <div class="collapse " id="customers">
                <nav class="nav nav-pills">
                    <a class="nav-link " href="Account">Hesaba ümumi baxış</a>
                    <a class="nav-link " href="#">Hesab parametrləri</a>
                </nav>
            </div>
        </div>

        <div class="nav-item">
            <a class="nav-link active" href="tarifs">
                <span class="material-symbols-outlined me-3">diamond</span> Tariflər
            </a>
        </div>

        <div class="nav-item">
            <a class="nav-link" href="balance">
                <span class="material-symbols-outlined me-3">sticky_note_2</span> Balans
                </a>
        </div>

        <div class="nav-item">
            <a class="nav-link " href="#">
                <span class="material-symbols-outlined me-3">deployed_code</span> Bildirişlər
                </a>
        </div>

    </nav>

    <h3 class="fs-base px-3 mb-4">Əməliyyatlar</h3>

    <nav class="navbar-nav nav-pills mb-xl-7">

        <div class="nav-item">
            <a class="nav-link " href="#" data-bs-toggle="collapse" data-bs-target="#questions" rol="button" aria-expanded="false" aria-controls="questions">
                <span class="material-symbols-outlined me-3">folder</span> Suallar
        </a>
            <div class="collapse " id="questions">
                <nav class="nav nav-pills">
                    <a class="nav-link " href="question">Sualları oxu</a>
                    <a class="nav-link " href="store">Market</a>
                </nav>
            </div>
        </div>

        <div class="nav-item">
            <a class="nav-link " href="#">
                <span class="material-symbols-outlined me-3">deployed_code</span> İmtahan keçmişi
            </a>
        </div>

    </nav>


    <!-- Divider -->
    <hr class="my-4 d-xl-none">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="server">
    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    <!-- Pricing options -->
    <div class="row mb-9">
        <div class="col-12 col-lg-4">
            <div class="card bg-body-tertiary border-transparent">
                <div class="card-body">
                    <h5 class="fs-5 mb-1">Basic</h5>
                    <div class="d-flex align-items-center mb-5">
                        <span class="fs-1 fw-semibold">0</span>
                        <span class="fs-3">₼</span>
                        <span class="fs-6 text-body-secondary ms-1">/ ∞ </span>
                    </div>
                    <ul class="list-group list-group-flush mb-5">
                        <li class="list-group-item bg-body-tertiary border-dark px-0">
                            <div class="row">
                                <div class="col text-body-secondary">Müddətsiz və icarəyə götürdüyünüz suallar aktivdir</div>
                                <div class="col-auto">
                                    <span class="material-symbols-outlined text-success">check</span>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-12 col-lg-4">
            <div class="card bg-body-tertiary border-transparent mt-3 mt-lg-0">
                <div class="card-body">
                    <h5 class="fs-5 mb-1">Pro</h5>
                    <p class="text-body-secondary">Mütəmadi istifadə edənlər üçün</p>
                    <div class="d-flex align-items-center mb-5">
                        <span class="fs-1 fw-semibold">5</span>
                        <span class="fs-3">₼</span>
                        <span class="fs-6 text-body-secondary ms-1">/ həftəlik</span>
                    </div>
                    <ul class="list-group list-group-flush mb-5">
                        <li class="list-group-item bg-body-tertiary border-dark px-0">
                            <div class="row">
                                <div class="col text-body-secondary">Bütün suallar aktivdir</div>
                                <div class="col-auto">
                                    <span class="material-symbols-outlined text-success">check</span>
                                </div>
                            </div>
                        </li>
                        <li class="list-group-item bg-body-tertiary border-dark px-0">
                            <div class="row">
                                <div class="col text-body-secondary">25 ədəd ödənişsiz imtahan</div>
                                <div class="col-auto">
                                    <span class="material-symbols-outlined text-success">check</span>
                                </div>
                            </div>
                        </li>
                    </ul>
                    <asp:Button ID="Pro" CssClass="btn btn-secondary d-block w-100" runat="server" Text="Get started" />
                </div>
            </div>
        </div>
        <div class="col-12 col-lg-4">
            <div class="card bg-body-tertiary border-transparent mt-3 mt-lg-0">
                <div class="card-body">
                    <h5 class="fs-5 mb-1">Enterprise</h5>
                    <p class="text-body-secondary">For major businesses with millions of transactions.</p>
                    <div class="d-flex align-items-center mb-5">
                        <span class="fs-1 fw-semibold">20</span>
                        <span class="fs-3">₼</span>
                        <span class="fs-6 text-body-secondary ms-1">/ aylıq</span>
                    </div>
                    <ul class="list-group list-group-flush mb-5">
                        <li class="list-group-item bg-body-tertiary border-dark px-0">
                            <div class="row">
                                <div class="col text-body-secondary">Bütün suallar aktivdir</div>
                                <div class="col-auto">
                                    <span class="material-symbols-outlined text-success">check</span>
                                </div>
                            </div>
                        </li>
                        <li class="list-group-item bg-body-tertiary border-dark px-0">
                            <div class="row">
                                <div class="col text-body-secondary">Limitsiz ödənişsiz imtahan</div>
                                <div class="col-auto">
                                    <span class="material-symbols-outlined text-success">check</span>
                                </div>
                            </div>
                        </li>
                    </ul>
                    <asp:Button ID="Enterprise" CssClass="btn btn-dark d-block w-100" runat="server" Text="Get started" />
                </div>
            </div>
        </div>
    </div>

    <!-- Header -->


    <!-- Accordion -->

</asp:Content>
