<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Cabinet.Master" CodeBehind="Question.aspx.vb" Inherits="new.az_ex.shop.WebForm4" %>

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
            <a class="nav-link active" href="#" data-bs-toggle="collapse" data-bs-target="#question" rol="button" aria-expanded="true" aria-controls="question">
                <span class="material-symbols-outlined me-3">folder</span> Suallar
                </a>
            <div class="collapse show" id="question">
                <nav class="nav nav-pills">
                    <a class="nav-link active" href="question">Suallara bax</a>
                    <a class="nav-link" href="store">Market</a>
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
    <script>
        function key() {
            var value = $("#search").val().toLowerCase();
            $("#ERT tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        }
    </script>

    <div class="row align-items-center mb-7">
        <div class="col-auto">
            <!-- Avatar -->
            <div class="avatar avatar-xl rounded text-primary">
                <i class="fa-solid fa-folder-open" style="font-size: 30px"></i>
            </div>
        </div>
        <div class="col">
            <!-- Breadcrumb -->
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb mb-2">
                    <li class="breadcrumb-item"><a class="text-body-secondary" href="#">Suallar</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Suallar toplusu</li>
                </ol>
            </nav>
            <h1 class="fs-4 mb-0">Suallar toplusu</h1>
        </div>
    </div>
    <div id="Cont" runat="server" visible="false">
        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
        <div class="card card-line bg-body-tertiary border-transparent mb-7">
            <div class="card-body p-4">
                <div class="row align-items-center">
                    <div class="col-12 col-lg-auto mb-3 mb-lg-0">
                        <div class="row align-items-center">
                            <div class="col-auto">
                                <div class="text-body-secondary" style="font-size: 16px">Sizin müvəqqəti (icarə) və müddətsiz olaraq aldığınız fənn sualları cədvəldə göstərilib.</div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-lg">
                        <div class="row gx-3">
                            <div class="col col-lg-auto ms-auto">
                                <div class="input-group bg-body">
                                    <input type="text" id="search" onkeyup="key()" class="form-control" placeholder="Search" aria-label="Search" aria-describedby="search">
                                    <span class="input-group-text">
                                        <span class="material-symbols-outlined">search</span>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="card card-body table-responsive" style="padding: 0px">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        </div>
    </div>
    <div id="Empty" runat="server" visible="true" class="row justify-content-center" style="width: 100%">
        <div class="col-12" style="max-width: 25rem">
            <h1 class="fs-1 text-center">😅</h1>
            <p class="lead text-center text-body-secondary">Sizin satın aldığınız fənn yoxdur.</p>
            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-secondary w-100" runat="server">Marketə keç</asp:LinkButton>
        </div>
    </div>

    <script>
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>

</asp:Content>
