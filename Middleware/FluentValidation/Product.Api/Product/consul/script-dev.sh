#!/bin/bash

service nginx start; 

# (nohup consul agent -config-dir /consul/config > /dev/null 2>&1 &); \
# sleep 10s; \
# nslookup *.server.dev.svc.cluster.local | grep 'Address: ' | awk {'print $2'} > ip.txt; \
# addresses="" 
# while read line;
# do
  # addresses+="$line ";
# done < ip.txt
# sleep 5s
# echo "addresses: $addresses"
# consul join $addresses; \

dotnet Api.dll;