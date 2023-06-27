document.addEventListener("DOMContentLoaded", async () => {

    const image = document.querySelector('#image');
    const name = document.querySelector("#name");

    const response = await fetch(`https://localhost:7212/api/Pokemon/detail/${location.search}`);

    const pokemon = await response.json();

    name.innerText = pokemon.name;
})