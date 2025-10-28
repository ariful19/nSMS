<%@ Page Title="Student Search" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="StudentSearch.aspx.cs" Inherits="Report_Viewer_StudentSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="importmap">
  {
    "imports": {
      "vue": "/Scripts/vue.esm-browser.js"
    }
  }
    </script>
    <style>
        .dtl {
            display: flex;
            flex-flow: column
        }

            .dtl > div {
                display: flex;
                padding: 0 .5em;
            }

                .dtl > div > div:first-child {
                    font-weight: bold;
                    flex: 2;
                }

                .dtl > div > div:last-child {
                    flex: 2;
                }

        thead th {
            cursor: pointer
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <div id="app">
        <div class="row">
            <div class="panel-heading">
                <h4>
                    <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Application,Criteria %>"></asp:Label></h4>
            </div>
            <div class="panel-body">
                <div class="col-lg-6 col-md-6">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label1" runat="server" Text="<%$ Resources:Application,Year %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlYear" runat="server" ClientIDMode="Static" DataTextField="Year" DataValueField="Id" CssClass="form-control dropdown" v-model="criteria.Year"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Application,Medium %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlMedium" runat="server" ClientIDMode="Static" DataTextField="MediumName" DataValueField="Id" CssClass="form-control dropdown" v-model="criteria.Medium"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="lblCampus" runat="server" Text="<%$ Resources:Application,Campus %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlCampus" runat="server" ClientIDMode="Static" DataTextField="CampusName" DataValueField="Id" CssClass="form-control dropdown" v-model="criteria.Campus"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Application,Class %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlClass" runat="server" ClientIDMode="Static" DataTextField="ClassName" DataValueField="Id" CssClass="form-control dropdown" v-model="criteria.Class"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label9" runat="server" Text="Student Name"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="stdName" runat="server" ClientIDMode="Static" CssClass="form-control dropdown" v-model="criteria.StudentName"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Application,Group %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlGroup" ClientIDMode="Static" runat="server" DataTextField="GroupName" DataValueField="Id" CssClass="form-control dropdown" v-model="criteria.Group"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Application,Shift %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlShift" ClientIDMode="Static" runat="server" DataTextField="Shift" DataValueField="Id" CssClass="form-control dropdown" v-model="criteria.Shift"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Application,Section %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlSection" ClientIDMode="Static" runat="server" DataTextField="Section" DataValueField="Id" v-model="criteria.Section" CssClass="form-control dropdown"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="tbxRegNo" ClientIDMode="Static" runat="server" CssClass="form-control dropdown" MaxLength="11" v-model="criteria.RegNo"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="display: flex; justify-content: center;">
            <input type="button" class="btn btn-primary" value="🔎 Search" @click="Search()"/>
        </div>
        <div class="row" v-if="list">
            <table class="table table-bordered table-responsive">
                <thead>
                    <tr>
                        <th @click="HeadClick('reg')">ID/Reg. No.</th>
                        <th @click="HeadClick('nem')">Name</th>
                        <th @click="HeadClick('class')">Class</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="i in list">
                        <td>{{i.RegNo}}</td>
                        <td>{{i.Name}}</td>
                        <td>{{i.ClassName}}</td>
                        <td>
                            <a href="javascript:void(0);" title="View Detail" @click="ViewDetail(i)"  data-toggle="modal" data-target="#myModal">
                                <i class="fa fa-2x fa-eye"></i>
                            </a>
                        </td>
                    </tr>
                </tbody>
                <tfoot></tfoot>
            </table>
        </div>
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content" v-if="SelectedStudent">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">{{SelectedStudent.NameEng}}</h4>
                    </div>
                    <div class="modal-body">
                        <div style="display: grid; grid-template-areas: 'left photo' 'left right'">
                            <div style="grid-area: left" class="dtl">
                                <div v-if="SelectedStudent.NameEng">
                                    <div>Name </div>
                                    <div>{{SelectedStudent.NameEng}}</div>
                                </div>
                                <div v-if="SelectedStudent.NameBan">
                                    <div>Name (Ban)</div>
                                    <div>{{SelectedStudent.NameBan}}</div>
                                </div>
                                <div v-if="SelectedStudent.FatherNameEng">
                                    <div>Father Name</div>
                                    <div>{{SelectedStudent.FatherNameEng}}</div>
                                </div>
                                <div v-if="SelectedStudent.FatherNameBan">
                                    <div>Father Name (Ban)</div>
                                    <div>{{SelectedStudent.FatherNameBan}}</div>
                                </div>
                                <div v-if="SelectedStudent.MotherNameEng">
                                    <div>Mother Name</div>
                                    <div>{{SelectedStudent.MotherNameEng}}</div>
                                </div>
                                <div v-if="SelectedStudent.MotherNameBan">
                                    <div>Mother Name (Ban)</div>
                                    <div>{{SelectedStudent.MotherNameBan}}</div>
                                </div>
                                <div v-if="SelectedStudent.Nationality">
                                    <div>Nationality</div>
                                    <div>{{SelectedStudent.Nationality}}</div>
                                </div>
                                <div v-if="SelectedStudent.DateofBirth">
                                    <div>Date of Birth</div>
                                    <div>{{SelectedStudent.DateofBirth}}</div>
                                </div>
                                <div v-if="SelectedStudent.PhoneNo">
                                    <div>Phone No</div>
                                    <div>{{SelectedStudent.PhoneNo}}</div>
                                </div>
                                <div v-if="SelectedStudent.Mobile">
                                    <div>Mobile</div>
                                    <div>{{SelectedStudent.Mobile}}</div>
                                </div>
                                <div v-if="SelectedStudent.PhoneHome">
                                    <div>Phone (Home)</div>
                                    <div>{{SelectedStudent.PhoneHome}}</div>
                                </div>
                                <div v-if="SelectedStudent.MobileHome">
                                    <div>Mobile (Home)</div>
                                    <div>{{SelectedStudent.MobileHome}}</div>
                                </div>
                                <div v-if="SelectedStudent.Email">
                                    <div>Email</div>
                                    <div>{{SelectedStudent.Email}}</div>
                                </div>
                                <div v-if="SelectedStudent.Fax">
                                    <div>Fax</div>
                                    <div>{{SelectedStudent.Fax}}</div>
                                </div>
                                <div v-if="SelectedStudent.BloodGroup">
                                    <div>Blood Group</div>
                                    <div>{{SelectedStudent.BloodGroup}}</div>
                                </div>
                                <div v-if="SelectedStudent.FatherNId">
                                    <div>Father NID</div>
                                    <div>{{SelectedStudent.FatherNId}}</div>
                                </div>
                                <div v-if="SelectedStudent.MotherNId">
                                    <div>Mother NID</div>
                                    <div>{{SelectedStudent.MotherNId}}</div>
                                </div>
                                <div v-if="SelectedStudent.IsFreedomFighter">
                                    <div>Freedom Fighter</div>
                                    <div>{{SelectedStudent.IsFreedomFighter}}</div>
                                </div>
                                <div v-if="SelectedStudent.IsTribal">
                                    <div>Tribal</div>
                                    <div>{{SelectedStudent.IsTribal}}</div>
                                </div>
                                <div v-if="SelectedStudent.IsPhysicallyDefect">
                                    <div>Physically Defect</div>
                                    <div>{{SelectedStudent.IsPhysicallyDefect}}</div>
                                </div>
                                <div v-if="SelectedStudent.FatherIncome">
                                    <div>Father Income</div>
                                    <div>{{SelectedStudent.FatherIncome}}</div>
                                </div>
                                <div v-if="SelectedStudent.MotherIncome">
                                    <div>Mother Income</div>
                                    <div>{{SelectedStudent.MotherIncome}}</div>
                                </div>
                                <div v-if="SelectedStudent.FatherPhone">
                                    <div>Father Phone</div>
                                    <div>{{SelectedStudent.FatherPhone}}</div>
                                </div>
                                <div v-if="SelectedStudent.MotherPhone">
                                    <div>Mother Phone</div>
                                    <div>{{SelectedStudent.MotherPhone}}</div>
                                </div>
                                <div v-if="SelectedStudent.BirthCertificate">
                                    <div>Birth Certificate</div>
                                    <div>{{SelectedStudent.BirthCertificate}}</div>
                                </div>
                                <div v-if="SelectedStudent.NameLocalGuardian1">
                                    <div>Name of Local Guardian1</div>
                                    <div>{{SelectedStudent.NameLocalGuardian1}}</div>
                                </div>
                                <div v-if="SelectedStudent.NameLocalGuardian2">
                                    <div>Name of Local Guardian2</div>
                                    <div>{{SelectedStudent.NameLocalGuardian2}}</div>
                                </div>
                                <div v-if="SelectedStudent.LocalGuardian1Mobile">
                                    <div>Local Guardian 1 Mobile</div>
                                    <div>{{SelectedStudent.LocalGuardian1Mobile}}</div>
                                </div>
                                <div v-if="SelectedStudent.LocalGuardian2Mobile">
                                    <div>Local Guardian 2 Mobile</div>
                                    <div>{{SelectedStudent.LocalGuardian2Mobile}}</div>
                                </div>
                                <div v-if="SelectedStudent.Religion">
                                    <div>Religion</div>
                                    <div>{{SelectedStudent.Religion}}</div>
                                </div>
                                <div v-if="SelectedStudent.RegNo">
                                    <div>Reg. No</div>
                                    <div>{{SelectedStudent.RegNo}}</div>
                                </div>
                                <div v-if="SelectedStudent.AdmissionDate">
                                    <div>Admission Date</div>
                                    <div>{{SelectedStudent.AdmissionDate}}</div>
                                </div>
                                <div v-if="SelectedStudent.AdmissionYear">
                                    <div>Admission Year</div>
                                    <div>{{SelectedStudent.AdmissionYear}}</div>
                                </div>
                                <div v-if="SelectedStudent.MediumName">
                                    <div>Medium Name</div>
                                    <div>{{SelectedStudent.MediumName}}</div>
                                </div>
                                <div v-if="SelectedStudent.ClassName">
                                    <div>Class Name</div>
                                    <div>{{SelectedStudent.ClassName}}</div>
                                </div>
                                <div v-if="SelectedStudent.GroupName">
                                    <div>Group Name</div>
                                    <div>{{SelectedStudent.GroupName}}</div>
                                </div>                             
                            </div>
                            <div style="grid-area: photo;display: flex; justify-content: center; margin: .5em;">
                                <img :src="'/Images/Student/'+SelectedStudent.PersonImage" style="height:200px;width:150px;" alt="Image"/>
                            </div>
                            <div style="grid-area: right" class="dtl">
                                   <div v-if="SelectedStudent.Shift">
                                    <div>Shift</div>
                                    <div>{{SelectedStudent.Shift}}</div>
                                </div>
                                <div v-if="SelectedStudent.Section">
                                    <div>Section</div>
                                    <div>{{SelectedStudent.Section}}</div>
                                </div>
                                <div v-if="SelectedStudent.CampusName">
                                    <div>Campus Name</div>
                                    <div>{{SelectedStudent.CampusName}}</div>
                                </div>
                                <div v-if="SelectedStudent.Year">
                                    <div>Year</div>
                                    <div>{{SelectedStudent.Year}}</div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script type="module">
        import { createApp } from 'vue'
        createApp({
            data() {
                return {
                    criteria: {
                        RegNo: "", Year: "", Medium: "", Campus: "", Class: "", StudentName: "", Group: "", Shift: "", Section: "", ExamType: ""
                    },
                    list: null,
                    SelectedStudent: null
                }
            },
            mounted() {

            },
            components: {

            },
            methods: {
                async Search() {
                    //console.log(JSON.stringify(this.criteria)); return;
                    wait();
                    var res = await fetch("/api/SM/StudentSearch", {
                        method: 'POST', // or 'PUT'
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(this.criteria),
                    });

                    var json = await res.json();
                    if (json) {
                        json.forEach(o => {
                            o["Name"] = `${o.NameEng} ${o.NameBan ? '(' + o.NameBan + ')' : ''}`;
                            o.DateofBirth = o.DateofBirth.split('T')[0];
                            o.AdmissionDate = o.AdmissionDate.split('T')[0];
                        });
                    }
                    this.list = json;
                    stopWaiting();
                },
                ViewDetail(i) {
                    this.SelectedStudent = i;
                },
                HeadClick(hn) {
                    switch (hn) {
                        case 'reg':
                            this.list = this.list.sort((a, b) => a.RegNo.localeCompare(b.RegNo));
                            break;
                        case 'nem':
                            this.list = this.list.sort((a, b) => a.Name.localeCompare(b.Name));
                            break;
                        case 'class':
                            this.list = this.list.sort((a, b) => a.ClassName.localeCompare(b.ClassName));
                            break;
                        default:
                    }
                },
                async btnPrintClick() {
                    var frm = new FormData();
                    var pmtSlip = document.getElementById("slipCont");
                    frm.append("BodyText", btoa(pmtSlip.innerHTML));
                    frm.append("HeadText", btoa(document.head.innerHTML));
                    var res = await fetch('/print.aspx?cmd=Student', {
                        method: 'POST',
                        body: frm,
                    });
                    if (res.ok) {
                        window.open("/print.aspx", "_blank");
                    }
                }
            }
        }).mount('#app')
    </script>
    <script>
        function wait() {
            var div = document.createElement("div");
            div.setAttribute("style", ` position: fixed;   width: 100%;  height: 100%;   top: 0;  left: 0;  right: 0;  bottom: 0;  background-color: rgba(0,0,0,0.5);  z-index: 999999;   cursor: pointer;    color: white;    font-size: 2em;    display: flex;    justify-content: center;    align-items: center;`);
            div.id = "waiter";
            div.innerHTML = "<div>Please Wait</div>"
            document.body.insertBefore(div, document.body.firstChild);
        }
        function stopWaiting() {
            document.getElementById("waiter").remove();
        }
    </script>
</asp:Content>

