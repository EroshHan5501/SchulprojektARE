document.addEventListener("DOMContentLoaded", async () => {

    const template = document.querySelector('#preview');
    const container = document.querySelector('#container');

    const pokemon = await requester("https://localhost:7212/api/Pokemon/");

    for (let p of pokemon) {
        const clone = template.content.cloneNode(true);

        console.log(p);
        console.log(p.name);
        console.log(p.images);
        console.log(clone);

        const name = clone.querySelector('#pokemon-name'),
            img = clone.querySelector('#pokemon-img');
        link = clone.querySelector('#pokemon-link')

        name.innerText = p.name;
        link.href = `/detail.html?pokemonId=${p.pokemonId}`;
        // for (let image of pokemon.images) {
        //     img.src = "https://de.m.wikipedia.org/wiki/Datei:Test-Logo.svg";
        // }

        container.appendChild(clone);
    }
})

async function requester(url) {
    const response = await fetch(url);

    const json = await response.json();

    return json;
}