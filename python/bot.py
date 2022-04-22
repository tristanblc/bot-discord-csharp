from datetime import datetime
from email import header
from email.headerregistry import ContentTypeHeader
import json
from urllib import request
import uuid
import requests
import time
import json
import certifi





requests.packages.urllib3.disable_warnings()

headers = {
    'accept': "*/*",
    'Content-Type' : "application/json"
}


with open('C:/Users/trito/desktop/shape.txt') as f:
    lines = f.readlines()
    i = 0 ;
    for line in lines :
       split_lines = line.split(",")
       if i == 0 :
         i = i + 1
         continue
       
       print(split_lines[1])

       payloads ={
         "id" : str(uuid.uuid4()),
         "lat" : float(str(split_lines[1])),
         "longit": float(str(split_lines[2])),
         "sequence": str(split_lines[3].split("\n")[0])

         
       }
       headers = {'content-type': 'application/json', 'Accept-Charset': 'UTF-8','User-agent': 'your bot 0.1'}


       y  = json.dumps(payloads)
       print(y)



       r = requests.post("https://localhost:7167/api/Shape",data=y,headers=headers, verify=False)

       print(r)
              
    

     