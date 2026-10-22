fetch("http://localhost:5000/api/events", {
    method: "GET",
    headers:
    {
        "Content-Type": "application/json",
        "x-api-key": "my-secret-api-key"
    },

})
    .then((response) => response.json())
    .then((data) => {
        console.log(data);
    });