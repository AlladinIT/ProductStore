# Product Store

## Requirements

For building and running the application you need:

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download)
- [Rider](https://www.jetbrains.com/rider/)


## Usage
During startup it will automatically populate database with some initial data: 3 stores, 2 group parent nodes, 2 group child nodes and 6 products.
This API provides 3 endpoints that will allow to add a product, return product/all products, return product groups as tree.
You can test those using [swagger](http://localhost:5231/swagger/index.html) or postman.    
Request examples:

`POST http://localhost:5231/api/v1/products`
```
{
  "productName": "NEW SAMSUNG PHONE",
  "price": 1000,
  "priceWithVAT": 1200,
  "VAtRate": 20,
  "GroupId": "33333333-3333-3333-3333-333333333333",
  "storesIds": [
    "11111111-1111-1111-1111-111111111111",
    "22222222-2222-2222-2222-222222222222"
  ]
}
```

`GET http://localhost:5231/api/v1/products?id=11111111-1111-1111-1111-111111111111`  
`GET http://localhost:5231/api/v1/products`

`GET http://localhost:5231/api/v1/groupstree`
