//

document.addEventListener("DOMContentLoaded", async () => {
// Get the current URL
    const url = new URL(window.location.href);

// Access specific parameter values
    const id = url.searchParams.get('pokemonId');
    const response = await fetch(`https://localhost:7212/api/Pokemon/detail/${id}`);

    document.getElementById("kp").innerText(response.kp);
    document.getElementById("number").innerText(response.number);
    document.getElementById("type").innerText(response.type);
    document.getElementById("height").innerText(response.height);
    document.getElementById("pokemonImage").innerText(response.pokemonImage);
    document.getElementById("basicstad").innerText(response.basicstad);
    document.getElementById("specialstad").innerText(response.specialstad);
    document.getElementById("extrastad").innerText(response.extrastad);

    let attacks = document.getElementById("attacks")
    for(let attack in response.attacks) {
        let newAttack = document.createElement('li');
        newAttack.innerText = attack;
        attacks.appendChild(newAttack)
    }

})

