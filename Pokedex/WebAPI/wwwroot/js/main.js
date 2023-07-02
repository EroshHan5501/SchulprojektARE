'use strict';

// Remove preload class once page is fully loaded

const template = document.querySelector("#card");

for (const p of pokemon) {
  const card = template.content.clone(true);

  const name= card.querySelectorAll("#name");
  name.innerText = p.name;
  body.appendChild(card);

  const kp = card.querySelectorAll("#kp");
  name.innerText = p.kp;
  body.appendChild(card);

  const image = card.querySelectorAll("#image");
  name.innerText = p.image;
  body.appendChild(card);

  const type  = card.querySelectorAll("#type");
  name.innerText = p.type;
  body.appendChild(card);

  const attacks = card.querySelectorAll("#attacks");
  name.innerText = p.attacks;
  body.appendChild(card);

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