//

document.addEventListener("DOMContentLoaded", async () => {
// Get the current URL
    const url = new URL(window.location.href);

// Access specific parameter values
    const id = url.searchParams.get('pokemonId');
    const response = await fetch(`https://localhost:7212/api/Pokemon/detail/${id}`);
    const json = await response.json();
    document.getElementById("kp").innerText(json.kp);
    document.getElementById("number").innerText(json.number);
    document.getElementById("type").innerText(json.type);
    document.getElementById("height").innerText(json.height);
    document.getElementById("pokemonImage").innerText(json.pokemonImage);
    document.getElementById("basicstad").innerText(json.basicstad);
    document.getElementById("specialstad").innerText(json.specialstad);
    document.getElementById("extrastad").innerText(json.extrastad);

    let attacks = document.getElementById("attacks")
    for(let attack in json.attacks) {
        let newAttack = document.createElement('li');
        newAttack.innerText = attack;
        attacks.appendChild(newAttack)
    }

})

