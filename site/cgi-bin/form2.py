#!/usr/bin/env python3
import cgi
import hashlib
import time
import pymysql
import pymysql.cursors

def decoding(text1, rime):
    filename = "log_" + rime.split('.')[1] + ".html"
    open("./logs/" + str(filename), "w")
    open("./logs/" + str(filename), "a+").write("LogID -> " + str(rime.split('.')[1]) + "<br>")
    open("./logs/" + str(filename), "a+").write("Hash -> " + str(text1) + "<br>")
    open("./logs/" + str(filename), "a+").write("Now hash in queue, wait....<br>")
    print("<p>Your log here -> {0}</p>".format("log_" + rime.split('.')[1] + ".html"))

def main():
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

def end():
    print("""</b></table>
        
        </div>
            
    </body>
    </html>""")
            
def db_query(text1, rime):
    connection = pymysql.connect(host='127.0.0.1',
                                user='hahs',
                                password='147147Zxc',
                                db='db_hasher',
                                charset='utf8mb4',
                                cursorclass=pymysql.cursors.DictCursor)
    try:
        with connection.cursor() as cursor:
            # Create a new record
            sql = "INSERT INTO queue (hash, log_id) VALUES ('{0}', '{1}');".format(text1, rime.split('.')[1])
            cursor.execute(sql)
            cursor.fetchone()
        connection.commit()
    finally:
        connection.close()



form = cgi.FieldStorage()
text1 = form.getfirst("TEXT_decode", "")
text2 = form.getfirst("TEXT_decode2", "не задано")
s = text1
b = s.encode("UTF-8")
main()
rime = str(time.time())
decoding(text1, rime)
db_query(text1, rime)
end()
