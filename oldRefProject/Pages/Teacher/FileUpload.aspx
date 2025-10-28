<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="Pages_Teacher_FileUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /* USER PROFILE PAGE */
        .card {
            margin-top: 20px;
            padding: 30px;
            background-color: rgba(214, 224, 226, 0.2);
            -webkit-border-top-left-radius: 5px;
            -moz-border-top-left-radius: 5px;
            border-top-left-radius: 5px;
            -webkit-border-top-right-radius: 5px;
            -moz-border-top-right-radius: 5px;
            border-top-right-radius: 5px;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
        }

            .card.hovercard {
                position: relative;
                padding-top: 0;
                overflow: hidden;
                text-align: center;
                background-color: #fff;
                background-color: rgba(255, 255, 255, 1);
            }

                .card.hovercard .card-background {
                    height: 130px;
                }

        .card-background img {
            -webkit-filter: blur(25px);
            -moz-filter: blur(25px);
            -o-filter: blur(25px);
            -ms-filter: blur(25px);
            filter: blur(25px);
            margin-left: -100px;
            margin-top: -200px;
            min-width: 130%;
        }

        .card.hovercard .useravatar {
            position: absolute;
            top: 15px;
            left: 0;
            right: 0;
        }

            .card.hovercard .useravatar img {
                width: 100px;
                height: 100px;
                max-width: 100px;
                max-height: 100px;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
                border-radius: 50%;
                border: 5px solid rgba(255, 255, 255, 0.5);
            }

        .card.hovercard .card-info {
            position: absolute;
            bottom: 14px;
            left: 0;
            right: 0;
        }

            .card.hovercard .card-info .card-title {
                padding: 0 5px;
                font-size: 20px;
                line-height: 1;
                color: #262626;
                background-color: rgba(255, 255, 255, 0.1);
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

        .card.hovercard .card-info {
            overflow: hidden;
            font-size: 12px;
            line-height: 20px;
            color: #737373;
            text-overflow: ellipsis;
        }

        .card.hovercard .bottom {
            padding: 0 20px;
            margin-bottom: 17px;
        }

        .btn-pref .btn {
            -webkit-border-radius: 0 !important;
        }

        .well .row {
            min-height: 400px;
            height: auto;
            overflow: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <div class="btn-pref btn-group btn-group-justified btn-group-lg" role="group" aria-label="...">
                    <div class="btn-group" role="group">
                        <button type="button" id="cover" class="btn btn-primary" href="#tabcover" data-toggle="tab">
                            Cover
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="roster" class="btn btn-default" href="#tabroster" data-toggle="tab">
                            Roster
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section1" class="btn btn-default" href="#tab1" data-toggle="tab">
                            Section 1
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section2" class="btn btn-default" href="#tab2" data-toggle="tab">
                            Section 2
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section3" class="btn btn-default" href="#tab3" data-toggle="tab">
                            Section 3
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section4" class="btn btn-default" href="#tab4" data-toggle="tab">
                            Section 4
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section5" class="btn btn-default" href="#tab5" data-toggle="tab">
                            Section 5
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section6" class="btn btn-default" href="#tab6" data-toggle="tab">
                            Section 6
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section7" class="btn btn-default" href="#tab7" data-toggle="tab">
                            Section 7
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section8" class="btn btn-default" href="#tab8" data-toggle="tab">
                            Section 8
                        </button>
                    </div>
                    <div class="btn-group" role="group">
                        <button type="button" id="section9" class="btn btn-default" href="#tab9" data-toggle="tab">
                            Section 9
                        </button>
                    </div>
                </div>

                <div class="well">
                    <div class="tab-content">
                        <div class="tab-pane fade in active" id="tabcover">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvCover" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" id="covern" href="#tabroster" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tabroster">
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvRoster" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="rosterp" href="#tabcover" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                            <button type="button" data-toggle="tab" id="rostern" href="#tab1" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tab1">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="panel with-nav-tabs panel-default">
                                        <div class="panel-heading">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#1A" data-toggle="tab">Part 1A</a></li>
                                                <li><a href="#1B" data-toggle="tab">Part 1B</a></li>
                                                <li><a href="#1C" data-toggle="tab">Part 1C1</a></li>
                                                <li><a href="#1D" data-toggle="tab">Part 1C2</a></li>
                                            </ul>
                                        </div>
                                        <div class="panel-body">
                                            <div class="tab-content">
                                                <div class="tab-pane fade in active" id="1A">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ID="gv1A" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="1B">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ID="gv1B" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="1C">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ID="gv1C" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane fade" id="1D">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:GridView ID="gv1D" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                                    <button type="button" data-toggle="tab" id="section1p" href="#tabroster" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                                    <button type="button" data-toggle="tab" id="section1n" href="#tab2" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                                    <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade in" id="tab2">
                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#2A" data-toggle="tab">Part 2A</a></li>
                                        <li><a href="#2B" data-toggle="tab">Part 2B1</a></li>
                                        <li><a href="#2C" data-toggle="tab">Part 2B2</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="2A">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv2A" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="2B">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv2B" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="2C">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv2C" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="section2p" href="#tab1" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                            <button type="button" data-toggle="tab" id="section2n" href="#tab3" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tab3">
                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#3A1" data-toggle="tab">Part 3A1</a></li>
                                        <li><a href="#3A2" data-toggle="tab">Part 3A2</a></li>
                                        <li><a href="#3A3" data-toggle="tab">Part 3A3</a></li>
                                        <li><a href="#3A4" data-toggle="tab">Part 3A4</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="3A1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv3A1" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="3A2">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv3A2" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="3A3">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv3A3" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="3A4">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv3A4" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="section3p" href="#tab2" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                            <button type="button" data-toggle="tab" id="section3n" href="#tab4" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tab4">
                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#4A" data-toggle="tab">Part 4A</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="4A">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv4A" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="section4p" href="#tab3" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                            <button type="button" data-toggle="tab" id="section4n" href="#tab5" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tab5">
                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#5A" data-toggle="tab">Part 5A</a></li>
                                        <li><a href="#5B" data-toggle="tab">Part 5B</a></li>
                                        <li><a href="#5C" data-toggle="tab">Part 5C</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="5A">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv5A" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="5B">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv5B" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="5C">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv5C" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="section5p" href="#tab4" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                            <button type="button" data-toggle="tab" id="section5n" href="#tab6" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tab6">
                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#6A" data-toggle="tab">Part 6A</a></li>
                                        <li><a href="#6B" data-toggle="tab">Part 6B</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="6A">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv6A" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="6B">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv6B" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="section6p" href="#tab5" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                            <button type="button" data-toggle="tab" id="section6n" href="#tab7" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tab7">
                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#7A" data-toggle="tab">Part 7A</a></li>
                                        <li><a href="#7B1" data-toggle="tab">Part 7B1</a></li>
                                        <li><a href="#7B2" data-toggle="tab">Part 7B2</a></li>
                                        <li><a href="#7C1" data-toggle="tab">Part 7C1</a></li>
                                        <li><a href="#7C2" data-toggle="tab">Part 7C2</a></li>
                                        <li><a href="#7D" data-toggle="tab">Part 7D</a></li>
                                        <li><a href="#7E" data-toggle="tab">Part 7E</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="7A">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv7A" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="7B1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv7B1" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="7B2">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv7B2" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="7C1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv7C1" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="7C2">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv7C2" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="7D">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv7D" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="7E">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv7E" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="section7p" href="#tab6" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                            <button type="button" data-toggle="tab" id="section7n" href="#tab8" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tab8">
                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#8A" data-toggle="tab">Part 8A</a></li>
                                        <li><a href="#8B" data-toggle="tab">Part 8B</a></li>
                                        <li><a href="#8C1" data-toggle="tab">Part 8C1</a></li>
                                        <li><a href="#8C2" data-toggle="tab">Part 8C2</a></li>
                                        <li><a href="#8D" data-toggle="tab">Part 8D</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="8A">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv8A" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="8B">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv8B" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="8C1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv8C1" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="8C2">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv8C2" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="8D">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv8D" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="section8p" href="#tab7" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                            <button type="button" data-toggle="tab" id="section8n" href="#tab9" class="btn btn-primary">Next <i class="fa fa-angle-double-right"></i></button>
                            <button type="button" data-toggle="tab" href="#tab9" class="btn btn-primary last">Last <i class="fa fa-fast-forward"></i></button>
                        </div>
                        <div class="tab-pane fade in" id="tab9">
                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#9A1" data-toggle="tab">Part 9A1</a></li>
                                        <li><a href="#9A2" data-toggle="tab">Part 9A2</a></li>
                                        <li><a href="#9B1" data-toggle="tab">Part 9B1</a></li>
                                        <li><a href="#9B2" data-toggle="tab">Part 9B2</a></li>
                                        <li><a href="#9C1" data-toggle="tab">Part 9C1</a></li>
                                        <li><a href="#9C2" data-toggle="tab">Part 9C2</a></li>
                                        <li><a href="#9D1" data-toggle="tab">Part 9D1</a></li>
                                        <li><a href="#9D2" data-toggle="tab">Part 9D2</a></li>
                                        <li><a href="#9D3" data-toggle="tab">Part 9D3</a></li>
                                        <li><a href="#9D4" data-toggle="tab">Part 9D4</a></li>
                                        <li><a href="#9E" data-toggle="tab">Part 9E</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="9A1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9A1" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9A2">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9A2" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9B1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9B1" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9B2">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9B2" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9C1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9C1" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9C2">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9C2" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9D1">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9D1" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9D2">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9D2" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9D3">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9D3" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9D4">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9D4" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="9E">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:GridView ID="gv9E" runat="server" ClientIDMode="Static" CssClass="table table-responsive table-hover"></asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <button type="button" data-toggle="tab" href="#tabcover" class="btn btn-primary first"><i class="fa fa-fast-backward"></i>First</button>
                            <button type="button" data-toggle="tab" id="section9p" href="#tab8" class="btn btn-primary"><i class="fa fa-angle-double-left"></i>Previous</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".btn").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                //$(".tab").addClass("active"); // instead of this do the below 
                $(this).removeClass("btn-default").addClass("btn-primary");
            });
            $(".last").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section9").removeClass("btn-default").addClass("btn-primary");
            });
            $(".first").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#cover").removeClass("btn-default").addClass("btn-primary");
            });
            $("#covern").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#roster").removeClass("btn-default").addClass("btn-primary");
            });
            $("#rosterp").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#cover").removeClass("btn-default").addClass("btn-primary");
            });
            $("#rostern").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section1").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section1p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#roster").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section1n").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section2").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section2p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section1").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section2n").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section3").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section3p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section2").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section3n").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section4").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section4p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section3").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section4n").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section5").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section5p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section4").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section5n").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section6").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section6p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section5").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section6n").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section7").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section7p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section6").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section7n").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section8").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section8p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section7").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section8n").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section9").removeClass("btn-default").addClass("btn-primary");
            });
            $("#section9p").click(function () {
                $(".btn-pref .btn").removeClass("btn-primary").addClass("btn-default");
                $("#section8").removeClass("btn-default").addClass("btn-primary");
            });
        });
    </script>
</asp:Content>

