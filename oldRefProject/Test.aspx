<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="assests/ckeditor/ckeditor.js"></script>
    <script src="assests/ckfinder/ckfinder.js"></script>

    
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-group">
            <label for="message" class=" col-sm-2 control-label">Article in English</label>
            <div class="col-lg-10">
                <textarea rows="10" name="article" id="article" class="form-control "></textarea>
            </div>

        </div>
        <div class="form-group">
            <label for="message" class=" col-sm-2 control-label">Article in Bengali</label>
            <div class="col-lg-10">
                <textarea rows="10" name="article1" id="article1" class="form-control "></textarea>
            </div>

        </div>
    </form>
</body>
</html>

  <script type="text/javascript">




      var a1 = CKEDITOR.replace('article');
      CKFinder.setupCKEditor(a1, '/ckfinder/');

      var a2 = CKEDITOR.replace('article1');
      CKFinder.setupCKEditor(a2, '/ckfinder/');

    </script>
  