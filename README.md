# TokenControl

This is a simple microservice that exposes a few methods in order to serve as an example and reference.

### CardControl 
    => add a new card to the user id. If the card already exists in the database, it will update the DateTime.
    It will return a JSON with the registration date, the card id, and the token.

### ValidateToken 
    => check if the token is valid (numbem and time)
    It will return true or false. 

### GetCards 
    => (extra function) Just return all the cards that are in the datasabe.

## Example usage

* POST

`https://[YOUR_URL]:[PORT]/api/CardControls/CardControl`

Parameters:
```json
    {
      "CustomerId":15,
      "Cardnumber":2299999999991232,
      "CVV":123
    }
```
`https://[YOUR_URL]:[PORT]/api/CardControls/ValidateToken`

Parameters:
```json
    {
      "CustomerId":15,
      "CardId":1,
      "Token":2322,
      "CVV":123
    }
```
* GET

`https://[YOUR_URL]:[PORT]/api/CardControls/GetCards`


## Swagger

To test the application you can use swagger.

`https://[YOUR_URL]:[PORT]/swagger/index.html`

## Nunit

I also used the NUNIT to make some unit test in the application. They are available in the 'TokenControl.Tests' project.
