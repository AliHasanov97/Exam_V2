<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Cabinet.Master" CodeBehind="Balance.aspx.vb" Inherits="new.az_ex.shop.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>İdarəetmə paneli</title>
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
                    <a class="nav-link " href="account">Hesaba ümumi baxış</a>
                    <a class="nav-link " href="#">Hesab parametrləri</a>
                </nav>
            </div>
        </div>

        <div class="nav-item">
            <a class="nav-link " href="tarifs">
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
    <div class="table-responsive">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </div>
</asp:Content>
