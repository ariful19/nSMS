export default {
    data() {
        return {
            count: 0,
            GTotal: 0,
            inWord: ""
        }
    },
    props: ['feeinfo', 'copy'],
    mounted() {
        console.log(this.feeinfo);
        if (this.feeinfo.FeeItems && this.feeinfo.FeeItems.length > 0) {
            this.GTotal = this.feeinfo.FeeItems.map(o => o.DueAmount).reduce((a, b) => a + b);
            this.inWord = toWords(this.GTotal);
        }
    },
    //ReceiptNo: "", Date: "", StudentName: "", IDNo: "", Class: "", Group: "", Section: "",  TutionFeeFor: "", TutionFee: 0, TutionLateFee: 0, HostelFeeFor: "", HostelFee: 0, HostelLateFee: 0, OtherFeeFor: 0, Version: 0, FeeItems=[]
    template: `
<div style='width:100%; padding: 0 1em;font-size: 0.8em;min-width: 30em;'>
   <div style="display:flex;width:100%;justify-content: space-around;">
      <img style="width: 6em;" src="http://www.queenscollege.edu.bd/Images/Common/School_logo.png"/>
      <div style="display:flex;flex-flow:column;">
         <div style=' font-size: 1.6em;'>Queen's School & College</div>
         <div style=" text-align: center; background: black; color: white;-webkit-print-color-adjust: exact;">_______</div>
      </div>
      <div style='margin-left: 0.5em; display:flex;align-items:center;flex-flow:column;'>
         <div style='background:black;color:white;-webkit-print-color-adjust: exact;'>{{copy}}</div>
         <div><span>{{feeinfo.Version=='English Version'?'o':'x'}}</span> English Version</div>
         <div><span>{{feeinfo.Version!='English Version'?'o':'x'}}</span> Bangla Version</div>
      </div>
   </div>
   <div style="display:flex;justify-content:space-between;">
      <div style="display:flex;">
         Receipt No. 
         <div style="border-bottom: solid thin;width:7em;padding-left: 0.5em;"> {{feeinfo.ReceiptNo}}</div>
      </div>
      <div style="display:flex;">
         Date 
         <div style="border-bottom: solid thin;width:7em;padding-left: 0.5em;"> {{feeinfo.Date}}</div>
      </div>
   </div>
   <div style="display:flex;margin-top:1em">
      <div>Student's Name</div>
      <div style="flex:9; border-bottom: solid thin;width:7em;text-align: center;">{{feeinfo.StudentName}}</div>
   </div>
   <div style="display:flex;margin-top:.5em">
      <div style="flex:10;display:flex;">
         <div style="flex:1">ID No.</div>
         <div style="border: solid thin;flex:3;text-align: center;">{{feeinfo.IDNo}}</div>
      </div>
      <div style="flex:9;display:flex;margin-left:1em">
         <div style="flex:1">Class</div>
         <div style="border-bottom: solid thin;flex:3">{{feeinfo.Class}}</div>
      </div>
   </div>
   <div style="display:flex;">
      <div style="flex:10;display:flex;">
         <div style="flex:1">Group</div>
         <div style="border-bottom: solid thin;flex:3">{{feeinfo.Group}}</div>
      </div>
      <div style="flex:9;display:flex;margin-left:1em">
         <div style="flex:1">Section</div>
         <div style="border-bottom: solid thin;flex:3">{{feeinfo.Section}}</div>
      </div>
   </div>
   <div style="display:flex;" v-for="i in feeinfo.FeeItems">
      <div style="flex:12;display:flex;">
         <div style="flex:2">{{i.PaymentType}}</div>
         <div style="border-bottom: solid thin;flex:3">{{i.MonthYear}}</div>
      </div>
      <div style="flex:7;display:flex;margin-left:1em">
         <div>Tk.</div>
         <div style="border-bottom: solid thin;flex:3;text-align: right;">{{i.DueAmount}}/-</div>
      </div>
   </div>
   <div style="width:100%;border-top:solid 2px; margin-top:.5em"></div>
   <div style="display:flex;">
      <div style="flex:12;display:flex;justify-content:right;font-weight:bold">G. Total</div>
      <div style="flex:7;display:flex;margin-left:1em">
         <div></div>
         <div style="border-bottom: solid thin;flex:3;text-align: right;">{{GTotal}}/-</div>
      </div>
   </div>
   <div style="display:flex;margin-top:.5em">
      <div>In Word Taka</div>
      <div style="flex:9; border-bottom: solid thin;width:7em;text-align: center;">{{inWord}} taka only</div>
   </div>
   <div style="display:flex;">
      <div style="flex:9; border-bottom: solid thin;width:7em;text-align: center;">{{GTotal}}/-</div>
      <div>In Cash</div>
   </div>
   <div style="display:flex;justify-content:space-around;margin-top:3em">
      <div style="display:flex;flex-flow:column;align-items:center">
         <div>Guardian/Student's </div>
         <div>Signature </div>
      </div>
      <div style="display:flex;flex-flow:column;align-items:center">
         <div>Receiver's </div>
         <div>Signature </div>
      </div>
   </div>
</div>
`
}
