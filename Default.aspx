<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function writecookie()
        {
            if(myform.txtcookie.value=="")
            {
                alert("Please set cookie");
                return;
            }
            cookievalue = escape(myform.txtcookie.value) + ";";
            document.cookie = "name=" + cookievalue;
            document.write("setting Cookies :"+"name="+cookievalue);
        }
        function readcookie()
        {
            var allcookie = document.cookie;
            document.write("All Cookie : " + allcookie + "<br/>");
            cookiearray = allcookie.split(";");
            for(var i=0;i<cookiearray.length;i++)
            {
                name = cookiearray[i].split("=")[0];
                value = cookiearray[i].split("=")[1];
                document.write("key is : " + name + "Value is : "+value);                    
            }
        }
        function setdate()
        {
         
            var now = new Date();
            now.setMonth(now.getMonth()  + 1);
            cookievalue = escape(myform.txtcookie.value) + ";";
            document.cookie = "name : " + cookievalue;
            document.cookie = "Expires =" + now.toUTCString()+";";
            document.write("setting Cookie " + cookievalue);


        }
        function getvalue() {
            var retval = prompt("Enter Your Name", "Your Name here");
            document.write("you are enterd" + "<br/>" + retval);
        }
        function book( Title, Author)
        {     
            this.Title = Title;
            this.Author = Author;
    
        }
        var mybook1 = new book("s", "g");
         // Define a function which will work as a method
        function addPrice(amount)
        {
             this.price = amount; 
        }         
        function book(title, author)
        {
            this.title = title;
            this.author  = author;
            this.addPrice = addPrice; // Assign that method as property.
        }

        var myBook = new book("Perl", "Mohtashim");
        myBook.addPrice(100);
        document.write(mybook1.title);
        document.write(mybook1.author);
        document.write("Book title is : " + myBook.title + "<br>");
        document.write("Book author is : " + myBook.author + "<br>");
        document.write("Book price is : " + myBook.price + "<br>");   
    </script>
</head>
<body>
    <form id="myform" runat="server">
    <div>
    <input type="text" name="txtcookie" id="txtcookie" />
        <input type="button" name="btncookie" id="btncookie" value="Set Cookie" onclick="book('s','g')"/>

    </div>
    </form>
</body>
</html>
