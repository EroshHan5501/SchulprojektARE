const testPokemon = {
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

document.addEventListener("DOMContentLoaded", async () => {

    const image = document.querySelector('#image');
    const name = document.querySelector("#name");
    const baseExperience = document.querySelector("#base-experience");
    const height = document.querySelector("#height");


    //const response = await fetch(`https://localhost:7212/api/Pokemon/detail/${location.search}`);

    const pokemon = testPokemon;

    name.innerText = pokemon.name;


    let isFront = false;
    image.src = pokemon.sprites.front_default;

    setInterval(() => {
        if (isFront) {
            image.src = pokemon.sprites.back_default;
            isFront = false;
        } else {
            image.src = pokemon.sprites.front_default;
            isFront = true;
        }
    }, 3000);

    baseExperience.innerText = pokemon.base_experience;
    height.innerText = pokemon.height;
})

