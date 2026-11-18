async function loadFish() {
    // 1. Вземаме стойностите от полетата
    let search = document.getElementById("search").value;
    let species = document.getElementById("speciesFilter").value;
    let sortOrder = document.getElementById("sortOrder").value;



    // 2. Подготвяме адреса на заявката
    let params = new URLSearchParams({
        search: search,
        species: species,
        sortOrder: sortOrder
    });



    // 3. Изпращаме заявката към контролера
    let response = await fetch(
        "/Fish/GetFilteredFish?" + params
    );



    // 4. Преобразуваме JSON отговора в JavaScript обекти
    let fishes = await response.json();



    // 5. Изчистваме старите редове от таблицата
    let tbody = document.getElementById("fishTable");
    tbody.innerHTML = "";



    // 6. Добавяме ред за всеки намерен лекар
    fishes.forEach(function (fish) {
        let row = document.createElement("tr");



        [fish.name, fish.species, fish.age, fish.tankId].forEach(function (value) {
            let cell = document.createElement("td");
            cell.textContent = value ?? "";
            row.appendChild(cell);
        });



        // Колона с линкове за действия
        let actions = document.createElement("td");



        ["Edit", "Details", "Delete"].forEach(function (action, index) {
            if (index > 0) {
                actions.appendChild(document.createTextNode(" | "));
            }



            let link = document.createElement("a");
            link.href = "/Fish/" + action + "/" + fish.id;
            link.textContent = action;
            actions.appendChild(link);
        });



        row.appendChild(actions);
        tbody.appendChild(row);
    });
}



// Автоматично филтриране
document.getElementById("search")
    .addEventListener("input", loadFish);



document.getElementById("speciesFilter")
    .addEventListener("input", loadFish);



document.getElementById("sortOrder")
    .addEventListener("change", loadFish);



// Изчистване на филтрите
document.getElementById("clearFilters")
    .addEventListener("click", function () {
        document.getElementById("search").value = "";
        document.getElementById("speciesFilter").value = "";
        document.getElementById("sortOrder").value = "name_asc";



        loadFish();
    });

