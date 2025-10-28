<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/AdminMaster.master" AutoEventWireup="true" CodeFile="PaymentSlip.aspx.cs" Inherits="Pages_Fees_PaymentSlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="importmap">
  {
    "imports": {
      "vue": "/Scripts/vue.esm-browser.js",
      "CompSlip": "/Scripts/vueComps/compPmtSlip.js?v=0.2"
    }
  }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="app" class="content">
        <div class="row" id="criterias">
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
                                <input type="text" class="form-control" v-model="criteria.StudentName"    list="dtlist" id="stdName" />
                                <datalist id="dtlist">
                                    <option v-for="i in stdList">{{i.NameEng}}</option>
                                </datalist>
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
                        <div style="display: flex; padding: 0 1em;">
                            <div class="form-group" style="flex:1">
                                <label for="ddlMonthFrom">From Month</label>
                                <select id="ddlMonthFrom" class="form-control"  style=" max-width: 14em;" v-model="criteria.FromMonth">
                                    <option>--Select--</option>
                                    <option v-for="m in months" :value="m[1]">{{m[0]}}</option>
                                </select>
                            </div>
                            <div class="form-group" style="flex:1">
                                <label>To Month</label>
                                <select id="ddlMonthTo" class="form-control" style=" max-width: 14em;" v-model="criteria.ToMonth">
                                    <option>--Select--</option>
                                    <option v-for="m in months" :value="m[1]">{{m[0]}}</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="inputEmail3" class="col-sm-4">
                                <asp:Label ID="LabelRoll" runat="server" Text="<%$ Resources:Application,RegNo %>"></asp:Label></label>
                            <div class="col-sm-6">
                               <%-- <asp:TextBox ID="tbxRegNo" ClientIDMode="Static" runat="server" CssClass="form-control dropdown" MaxLength="11" v-model="criteria.RegNo" list="dtlistStudents"></asp:TextBox>
                                <datalist id="dtlistStudents">
                                    <option v-for="i in stdList">{{i.RegNo}}</option>
                                </datalist>--%>
                                <select v-model="criteria.RegNo" class="form-control" id="selReg">
                                    <option v-for="i in stdList" :value="i.RegNo">{{i.RegNo}}</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
        <div class="row" style="display:flex; justify-content:center">
            <input type="button" value="Load" @click="LoadDueList()" class="btn btn-success"/>
        </div>
        <div class="row" v-if="dueList" style="margin: 1em;background: white;filter: drop-shadow(1px 1px 2px black);border-radius: .5em;">
            <table class="table table-responsive">
                <thead>
                    <tr>
                        <th>Payment Type</th>
                        <th>Month-Year</th>
                        <th>Month</th>
                        <th>Amount</th>
                        <th>Total Given</th>
                        <th>Due Amount</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="i in dueList">
                        <td>{{i.PaymentType}}</td>
                        <td>{{i.MonthYear}}</td>
                        <td>{{i.MonthName}}</td>
                        <td>{{i.Payable}}</td>
                        <td>{{i.TotalGiven?i.TotalGiven:0}}</td>
                        <td>{{i.DueAmount?i.DueAmount:0}}</td>
                        <td>
                            <input type="checkbox" v-model="i.Selected" @change="SelectionChange(i)"/>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="row" style="overflow:scroll" id="slipCont" v-if="!hidden">
            <div style="display: flex; flex-flow: column">
                <div style="display: flex;">
                    <comp-pmt-slip :feeinfo="feeinfo" copy="STUDENT COPY"></comp-pmt-slip>
                    <comp-pmt-slip :feeinfo="feeinfo" copy="OFFICE COPY"></comp-pmt-slip>
                </div>
                <div style="display: flex;margin-top:3em">
                    <comp-pmt-slip :feeinfo="feeinfo" copy="Teacher's Copy"></comp-pmt-slip>
                    <comp-pmt-slip :feeinfo="feeinfo" copy="BANK COPY"></comp-pmt-slip>
                </div>
            </div>
        </div>
        <div>
            <input type="button" name="btnPrint" value="Print" class="btn btn-primary" @click="btnPrintClick()" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <%var data = new PmtSlipInfo(); %>
    <script src="/Scripts/n2w.js"></script>
    <script>
        var recpNo = '<%=Session["recpNo"] %>';
        var date = '<%=DateTime.Now.ToString("dd/MM/yyyy") %>';
        window.addEventListener('load', () => {
            var prnt = document.getElementById('criterias');
            var elms = prnt.querySelectorAll("select");
            elms.forEach(i => {
                if (i.id == "selReg") return;
                i.addEventListener("change", async (e) => {
                    vapp.stdList = [];
                    var res = await fetch("/api/SM/StudentSearch", {
                        method: 'POST', // or 'PUT'
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(vapp.criteria),
                    });
                    vapp.criteria.RegNo = "";
                    vapp.stdList = await res.json();
                });
            })
        })
    </script>
    <script type="module">
        import { createApp } from 'vue'
        import compPmtSlip from 'CompSlip'

        createApp({
            data() {
                return {
                    criteria: {
                        RegNo: "", Year: "", Medium: "", Campus: "", Class: "", StudentName: "", Group: "", Shift: "", Section: "", ExamType: "", FromMonth: new Date().getMonth().toString(), ToMonth: new Date().getMonth().toString(), Limit: 50
                    },
                    feeinfo: {
                        ReceiptNo: recpNo, Date: date, StudentName: "", IDNo: "", Class: "", Group: "", Section: "", TutionFeeFor: "", TutionFee: parseFloat(0), TutionLateFee: 0, HostelFeeFor: "", HostelFee: parseFloat(0), HostelLateFee: 0, OtherFeeFor: "", OtherFee: parseFloat(0), Version: "", FeeItems: []
                    },
                    months: [["January", 0], ["February", 1], ["March", 2], ["April", 3], ["May", 4], ["June", 5], ["July", 6], ["August", 7], ["September", 8], ["October", 9], ["November", 10], ["December", 11]],
                    stdList: [],
                    dueList: null,
                    hidden: false
                }
            },
            components: {
                compPmtSlip
            },
            mounted() {
                window["vapp"] = this;
            },
            methods: {
                async LoadDueList() {
                    if (!(this.criteria.RegNo || this.criteria.StudentName)) {
                        alert("Please select a name or Reg No");
                        return;
                    }
                    var res = await fetch("/api/SM/StudentDueList", {
                        method: 'POST', // or 'PUT'
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(this.criteria)
                    });
                    var data = await res.json();
                    data.forEach(o => {
                        var mm = parseInt(o.PMonth);
                        o["MonthName"] = mm > 0 ? this.months.find(m => m[1] == mm - 1)[0] : "Academic";
                        o["Selected"] = false;
                        o.DueAmount = o.DueAmount ? o.DueAmount : o.Payable - (o.TotalGiven ? o.TotalGiven : 0);
                    });
                    data = data.sort((a, b) => parseInt(a.PMonth) - parseInt(b.PMonth));
                    this.dueList = data;
                },
                SelectionChange(i) {
                    var selecteds = this.dueList.filter(o => o.Selected);
                    if (selecteds && selecteds.length > 0) {
                        var first = selecteds[0];
                        this.feeinfo.StudentName = first.NameEng; this.feeinfo.IDNo = first.RegNo; this.feeinfo.Class = first.ClassName; this.feeinfo.Group = first.GroupName; this.feeinfo.Section = first.Section; this.feeinfo.Version = first.MediumName; this.feeinfo.FeeItems = selecteds;
                    }
                    this.hidden = true;
                    setTimeout(() => this.hidden = false, 200);
                },
                async btnPrintClick() {
                    var frm = new FormData();
                    var pmtSlip = document.getElementById("slipCont");
                    frm.append("BodyText", btoa(pmtSlip.innerHTML));
                    frm.append("HeadText", btoa(document.head.innerHTML));
                    var res = await fetch('/print.aspx?cmd=UpdPmtSlpNo', {
                        method: 'POST',
                        body: frm,
                    });
                    if (res.ok) {
                        window.open("/print.aspx", "_blank");
                    }
                },
                OnRegChange() {
                    console.log("oitto");
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

