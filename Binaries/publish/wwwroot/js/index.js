document.addEventListener("DOMContentLoaded", async () => {

    const template = document.querySelector('#preview');
    const container = document.querySelector('#container');

    // const pokemon = await requester("https://localhost:7212/api/Pokemon/");

    const pokemon = moduleGenerator(10);

    for (let p of pokemon) {
        const clone = template.content.cloneNode(true);

        const name = clone.querySelector('#pokemon-name'),
            img = clone.querySelector('#pokemon-img'),
            link = clone.querySelector('#pokemon-link'),
            height = clone.querySelector('#height'),
            baseExperience = clone.querySelector('#base-experience');

        name.innerText = `${p.id}. ${p.name}`;
        link.href = `/detail.html?pokemonId=${p.id}`;

        img.src = p.sprites.front_default;

        height.innerText = p.height;
        baseExperience.innerText = p.base_experience;

        container.appendChild(clone);
    }
})



function moduleGenerator(n) {
    const pokemon = {
        id: 1,
        name: "Hello",
        url: "https://hello.pokemon.com/",
        base_experience: 23,
        height: 2,
        abilities: [
            {
                ability: {
                    name: "Attacking",
                    url: "https://attack.com/"
                }
            }
        ],
        stats: [
            {
                base_stat: 235,
                effort: 23,
                stat: {
                    name: "hunger",
                    url: "https://hunger.com/"
                }
            }
        ],
        types: [
            {
                slot: 34,
                type: {
                    name: "water",
                    url: "https://water.com"
                }
            }
        ],
        moves: [
            {
                move: {
                    name: "dancing",
                    url: "https://dancing.com/"
                }
            }
        ],
        sprites: {
            front_default: "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/132.png",
            back_default: "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/132.png"
        }
    }

    pokemonList = []
    for (let i = 0; i < n; i++) {
        pokemonList.push(pokemon);
    }

    return pokemonList;
}