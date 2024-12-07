<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Cabinet.Master" CodeBehind="account.aspx.vb" Inherits="new.az_ex.shop.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>İdarəetmə paneli</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
   <div class="row">
          <div class="col-12 col-xxl-4">
            <div class="position-sticky mb-8" style="top: 40px">
              <!-- Card -->
              <div class="card bg-body mb-3">
                <!-- Image -->
                <div id="GTX" runat="server" class="card-img-top pb-13" style="background: no-repeat url(../assets/img/backgrounds/background-1.jpg) center center / cover"></div>

                <!-- Avatar -->
                <div class="avatar avatar-xl rounded-circle mt-n7 mx-auto">
                    <asp:Image ID="Avatar" runat="server" CssClass="avatar-img border border-white border-3" />
                </div>

                <!-- Body -->
                <div class="card-body text-center">
                  <!-- Heading -->
                  <h1 class="card-title fs-5" id="Name" runat="server">John Williams</h1>

                  <!-- Text -->
                  <p id="About" runat="server" class="text-body-secondary mb-6">James is a long-standing customer with a passion for technology.</p>

                  <!-- List -->
                  <ul class="list-group list-group-flush mb-0">
                    <li class="list-group-item d-flex align-items-center justify-content-between bg-body px-0">
                      <span class="text-body-secondary">İşiniz</span>
                      <span id ="Job" runat="server">TechPinnacle Solutions</span>
                    </li>
                    <li class="list-group-item d-flex align-items-center justify-content-between bg-body px-0">
                      <span class="text-body-secondary">Email</span>
                      <span id ="Email" runat="server">TechPinnacle Solutions</span>
                    </li>
                  </ul>
                </div>
              </div>

              <!-- Buttons -->
              <div class="row gx-3">
                <div class="col">
                  <button class="btn btn-light w-100">Balansı artır</button>
                </div>
                <div class="col">
                  <button class="btn btn-light w-100">Əməliyyatlar</button>
                </div>
              </div>
            </div>
          </div>
          <div class="col-12 col-xxl">
            <section class="mb-8">
              <!-- Header -->
              <div class="d-flex align-items-center justify-content-between mb-5">
                <h2 class="fs-5 mb-0">Recent orders</h2>
               
                </div>

              <!-- Table -->

              <div class="table-responsive">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
              </div>
            </section>
            <section>
              <!-- Header -->
              <div class="row align-items-center justify-content-between mb-5">
                <div class="col">
                  <h2 class="fs-5 mb-0">Files</h2>
                </div>
                <div class="col-auto">
                  <a class="btn btn-light" href="#"> <span class="material-symbols-outlined text-body-secondary me-1">upload</span> Upload </a>
                </div>
              </div>

              <!-- Table -->
              <div class="table-responsive">
                <table class="table align-middle mb-0">
                  <tbody>
                    <tr>
                      <td>
                        <div class="d-flex align-items-center">
                          <div class="avatar rounded text-primary">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" class="fs-4 duo-icon duo-icon-id-card" data-duoicon="id-card"><path fill="currentColor" d="M20 3a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h16Z" class="duoicon-secondary-layer" opacity=".3"></path><path fill="currentColor" d="M10 9v2H8V9h2Zm7 2h-3a1 1 0 0 0-.117 1.993L14 13h3a1 1 0 0 0 .117-1.993L17 11Z" class="duoicon-primary-layer"></path><path fill="currentColor" d="M10 7H8a2 2 0 0 0-1.995 1.85L6 9v2a2 2 0 0 0 1.85 1.995L8 13h2a2 2 0 0 0 1.995-1.85L12 11V9a2 2 0 0 0-1.85-1.995L10 7Zm7 8H7a1 1 0 1 0 0 2h10a1 1 0 1 0 0-2Z" class="duoicon-primary-layer"></path></svg>
                          </div>
                          <div class="ms-4">
                            <div class="fw-normal">invoice.pdf</div>
                            <div class="fs-sm text-body-secondary">1.5mb · PNG</div>
                          </div>
                        </div>
                      </td>
                      <td class="text-body-secondary">Uploaded on Mar 01, 2024</td>
                      <td style="width: 0">
                        <button class="btn btn-sm btn-light">Download</button>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <div class="d-flex align-items-center">
                          <div class="avatar rounded text-primary">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" class="fs-4 duo-icon duo-icon-id-card" data-duoicon="id-card"><path fill="currentColor" d="M20 3a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h16Z" class="duoicon-secondary-layer" opacity=".3"></path><path fill="currentColor" d="M10 9v2H8V9h2Zm7 2h-3a1 1 0 0 0-.117 1.993L14 13h3a1 1 0 0 0 .117-1.993L17 11Z" class="duoicon-primary-layer"></path><path fill="currentColor" d="M10 7H8a2 2 0 0 0-1.995 1.85L6 9v2a2 2 0 0 0 1.85 1.995L8 13h2a2 2 0 0 0 1.995-1.85L12 11V9a2 2 0 0 0-1.85-1.995L10 7Zm7 8H7a1 1 0 1 0 0 2h10a1 1 0 1 0 0-2Z" class="duoicon-primary-layer"></path></svg>
                          </div>
                          <div class="ms-4">
                            <div class="fw-normal">agreement_123.pdf</div>
                            <div class="fs-sm text-body-secondary">3.7mb · PDF</div>
                          </div>
                        </div>
                      </td>
                      <td class="text-body-secondary">Updated on Mar 03, 2024</td>
                      <td style="width: 0">
                        <button class="btn btn-sm btn-light">Download</button>
                      </td>
                    </tr>
                    <tr>
                      <td>
                        <div class="d-flex align-items-center">
                          <div class="avatar rounded text-primary">
                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" class="fs-4 duo-icon duo-icon-id-card" data-duoicon="id-card"><path fill="currentColor" d="M20 3a2 2 0 0 1 2 2v14a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h16Z" class="duoicon-secondary-layer" opacity=".3"></path><path fill="currentColor" d="M10 9v2H8V9h2Zm7 2h-3a1 1 0 0 0-.117 1.993L14 13h3a1 1 0 0 0 .117-1.993L17 11Z" class="duoicon-primary-layer"></path><path fill="currentColor" d="M10 7H8a2 2 0 0 0-1.995 1.85L6 9v2a2 2 0 0 0 1.85 1.995L8 13h2a2 2 0 0 0 1.995-1.85L12 11V9a2 2 0 0 0-1.85-1.995L10 7Zm7 8H7a1 1 0 1 0 0 2h10a1 1 0 1 0 0-2Z" class="duoicon-primary-layer"></path></svg>
                          </div>
                          <div class="ms-4">
                            <div class="fw-normal">receipt_456.pdf</div>
                            <div class="fs-sm text-body-secondary">2.1mb · PDF</div>
                          </div>
                        </div>
                      </td>
                      <td class="text-body-secondary">Uploaded on Mar 05, 2024</td>
                      <td style="width: 0">
                        <button class="btn btn-sm btn-light">Download</button>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </section>
          </div>
       <div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Menu" runat="server">
    <h3 class="fs-base px-3 mb-4">İdarəetmə paneli</h3>

    <!-- Nav -->
    <nav class="navbar-nav nav-pills mb-xl-7">

     <div class="nav-item">
                <a class="nav-link active" href="#" data-bs-toggle="collapse" data-bs-target="#account" rol="button" aria-expanded="true" aria-controls="account">
                  <span class="material-symbols-outlined me-3">person</span> Hesab
                </a>
                <div class="collapse show" id="account">
                  <nav class="nav nav-pills">
                    <a class="nav-link active" href="account">Hesaba ümumi baxış</a>
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

    <!-- Nav -->
</asp:Content>
