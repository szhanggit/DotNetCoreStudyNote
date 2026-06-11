$Url = "https://localhost:44359/api/TenantShard"

$Body = @{
  dbName= "txc_local_tenant_th"
  key= "1"
}
Invoke-RestMethod -Method 'Post' -Uri $url -Body ($Body|ConvertTo-Json) -ContentType "application/json"

$Body = @{
  dbName= "txc_local_tenant_in"
  key= "2"
}
Invoke-RestMethod -Method 'Post' -Uri $url -Body ($Body|ConvertTo-Json) -ContentType "application/json"

$Body = @{
  dbName= "txc_local_tenant_id"
  key= "3"
}
Invoke-RestMethod -Method 'Post' -Uri $url -Body ($Body|ConvertTo-Json) -ContentType "application/json"

$Body = @{
  dbName= "txc_local_tenant_jp"
  key= "4"
}
Invoke-RestMethod -Method 'Post' -Uri $url -Body ($Body|ConvertTo-Json) -ContentType "application/json"

$Body = @{
  dbName= "txc_local_tenant_gr"
  key= "5"
}
Invoke-RestMethod -Method 'Post' -Uri $url -Body ($Body|ConvertTo-Json) -ContentType "application/json"

$Body = @{
  dbName= "txc_local_tenant_sg"
  key= "6"
}
Invoke-RestMethod -Method 'Post' -Uri $url -Body ($Body|ConvertTo-Json) -ContentType "application/json"

$Body = @{
  dbName= "txc_local_tenant_tw"
  key= "7"
}
Invoke-RestMethod -Method 'Post' -Uri $url -Body ($Body|ConvertTo-Json) -ContentType "application/json"

$Body = @{
  dbName= "txc_local_tenant_ma"
  key= "8"
}
Invoke-RestMethod -Method 'Post' -Uri $url -Body ($Body|ConvertTo-Json) -ContentType "application/json"