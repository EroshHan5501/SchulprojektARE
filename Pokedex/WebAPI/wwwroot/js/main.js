'use strict';

// Remove preload class once page is fully loaded
document.addEventListener("DOMContentLoaded", async () => {
    const response = await fetch("https://localhost:7212/api/Pokemon/");
    const json = await response.json();
	console.log('JSON HIER');
	console.log(json);
    const template = document.querySelector("#card");
    const container = document.querySelector("#container");

    for (const p of json) {
      const card = template.content.cloneNode(true);

      console.log(p);
      const name= card.querySelector("#name");
      name.innerText = p.name;
    
      // const kp = card.querySelectorAll("#kp");
      // name.innerText = p.kp;
    
      const image = card.querySelector("#image");
      image.src = p.sprites.front_default;
      
      const baseExperience = card.querySelector("#base-experience");
      baseExperience.innerText += p.base_experience;
      
      const height = card.querySelector("#height");
      height.innerText += p.height;

      const detailLink = card.querySelector("#detail-link");
      detailLink.href = `https://localhost:7212/Detail.html?pokemonId=${p.id}`;
      
      container.appendChild(card);
    }

});

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

  const pokemonList = []
  for (let i = 0; i < n; i++) {
      pokemonList.push(pokemon);
  }

  return pokemonList;
}




window.addEventListener('load', function() {
  Array.from(document.getElementsByTagName('body')).forEach(function(el) {
    el.classList.remove('preload');
  });
});

// Add class to navigation when scrolling down

document.addEventListener('scroll', function() {
  const header = document.querySelector('.header-main');
  if (window.scrollY >= 20) {
    header.classList.add('fade-in');
  } else {
    header.classList.remove('fade-in');
  }
});

// Add class when mobile navigation icon is clicked

Array.from(document.getElementsByClassName('nav-toggle')).forEach(function(el) {
  el.addEventListener('click', function() {
    Array.from(document.getElementsByTagName('body')).forEach(function(el) {
      el.classList.toggle('no-scroll');
    });
    Array.from(document.getElementsByClassName('header-main')).forEach(function(el) {
      el.classList.toggle('active');
    });
  });
});

// Prevent background from scrolling on mobile when navigation is toggled

document.addEventListener('touchmove', function(evt) {
  evt.preventDefault();
});