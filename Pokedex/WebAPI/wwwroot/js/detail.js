//

document.addEventListener("DOMContentLoaded", async () => {
// Get the current URL
    const url = new URL(window.location.href);

// Access specific parameter values
    const id = url.searchParams.get('pokemonId');
    const response = await fetch(`https://localhost:7212/api/Pokemon/detail/${id}`);
    const json = await response.json();
    document.getElementById("number").innerText("Nummer: "+json.id);
    document.getElementById("type").innerText("Typ: " + json.type);
    document.getElementById("height").innerText("Größe: " + json.height);
    document.getElementById("pokemonImage").setAttribute("src", json.sprites.front_default);

    document.getElementById("kp").innerText(json.hp);
    document.getElementById("attackstat").innerText(json.hp);
    document.getElementById("defstat").innerText(json.hp);
    document.getElementById("spattackstat").innerText(json.hp);
    document.getElementById("spdefstat").innerText(json.hp);
    document.getElementById("speedstat").innerText(json.hp);

    let abilities = json.abilis;
    let abilityList = document.getElementById("abilities");
    for (let ability in abilities) {
        let ab = document.createElement("li");
        ab.innerText = ability.name + " | " + ability.entry;
        abilityList.appendChild(ab);
    }

    let attacks = document.getElementById("attacks")
    for(let attack in json.movement) {
        let newAttack = document.createElement('li');
        newAttack.innerText = attack.name;
        attacks.appendChild(newAttack)
    }

})

