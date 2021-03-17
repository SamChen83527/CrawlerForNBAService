# NBA Crawler Service
A web crawler service for NBA players' career statistics

## Run
WIN APP

Click ```/CrawlerForNBA/bin/Release/netcoreapp3.1/publish/CrawlerForNBA.exe```

CMD
```bash
cd /CrawlerForNBA/bin/Release/netcoreapp3.1/publish

dotnet CrawlerForNBA.dll
```

## Get player career statistics csv file
- req
```
GET /players/{alphabet}
```
- req query parameters

| Name     | Required/optional | Type   | Description           |
| -------- |:----------------- |:------ |:--------------------- |
| alphabet | Required          | String | Alphabet from a to z. |


- resp
```json
200 Ok

{Alphabet}.csv
```


```json
500 Internal Server Error
```