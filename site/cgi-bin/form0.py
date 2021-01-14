#!/usr/bin/env python3
import cgi
import base64


form = cgi.FieldStorage()
text1 = form.getfirst("TEXT_1", "не задано")

s = text1
b = s.encode("UTF-8")
e = base64.b64encode(b)
d = str(e).replace('b\'', '')
r = str(d).replace('\'','')

print("Content-type: text/html\n")
print("""<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
        <title>Hash</title>
        
        <!-- <style type="text/css">    
           * { margin: 0; padding: 0; }
           p { padding: 10px; }
           #left { position: absolute; left: 0; top: 0; width: 10%; }
           #right { position: absolute; right: 0; top: 0; width: 90%; } 
        </style> -->
        <style>
          .block-left{width:20%;height:800px;overflow:auto;float:left;}
          .block-right{width:80%;height:800px;overflow:auto;}
          a{padding-left: 10px;}
        </style>
</head>

<body>
    <div id="left" class="block-left">
      <table border="1" width="100%" cellpadding="1">
        <tr><caption>Menu</caption></tr>
        <tr><th><a href='../index.html'>Home page</a></th></tr>
        <tr><th><a href='../base64.html'>Base 64</a></th></tr>
        <tr><th><a href='../md5_encode.html'>MD5 Encode</a></th></tr>
        <tr><th><a href='../md5_decode.html'>MD5 Decode</a></th></tr>
      </table>
    </div>

    <div id="right" class="block-right">
        <b>
        <table border="1">
        """)
print("<p>Result of encoding: {}</p>".format(str(r)))

print("""</b></table>
      
    </div>
        
</body>
</html>""")