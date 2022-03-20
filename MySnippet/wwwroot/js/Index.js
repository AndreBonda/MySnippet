import { HTTP } from './Http/HTTP.js';

const http = new HTTP;

window.addEventListener('DOMContentLoaded', () => {
    var idSnippetSelected = 0;

    // ricerca gli snippet ad ogni lettera inserita nella barra di ricerca
    document.getElementById("snippet-search").addEventListener("keyup", e => {
        var paramValue = encodeURIComponent(e.target.value);
        http.get("/Home/GetSnippets?filter=" + paramValue)
            .then(data => {
                if (data.status === 200) {
                    if (data !== null && data.snippets.length > 0) {
                        document.getElementById("salva").disabled = false;
                        document.getElementById("copy-clipboard").disabled = false;
                        document.getElementById("delete-btn").disabled = false;
                        let output = "";
                        for (let snippet of data.snippets) {
                            let classList = "list-group-item p-1";

                            output += `
                            <li class="${classList}" data-id="${snippet.id}" >
                                <h6 class="list-snippet-title">${snippet.title}</h6>
                                <p class="list-snippet-descr mb-0">${snippet.description}</p>
                            </li>`;

                            document.getElementById("snippets-list").innerHTML = output;
                        }
                        addClickEventToSnippets();
                        getSnippet(idSnippetSelected);
                    } else {
                        document.getElementById("snippets-list").innerHTML = ``;
                        document.getElementById("salva").disabled = true;
                        document.getElementById("copy-clipboard").disabled = true;
                        document.getElementById("delete-btn").disabled = true;
                    }
                } else {
                    alert(data.msg);
                }
            })
            .catch(err => alert(err));
    });

    // salva snippet
    document.getElementById("salva").addEventListener('click', e => {
        if (idSnippetSelected > 0) {
            const title = document.getElementById("snippet-title").value;
            const description = document.getElementById("snippet-description").value;
            const content = document.getElementById("snippet-content").value;

            const snippet = {
                Id: idSnippetSelected,
                Title: title,
                Description: description,
                Content: content
            };

            http.post("/Home/UpdateSnippet", snippet)
                .then(data => {
                    if (data.status === 200)
                    {
                        var itemSelected = document.querySelector(".list-group-item-selected");
                        itemSelected.querySelector(".list-snippet-title").innerText = document.getElementById("snippet-title").value;
                        itemSelected.querySelector(".list-snippet-descr").innerText = document.getElementById("snippet-description").value;

                    } else {
                        alert(data.msg);
                    }
                })
                .catch(err => alert(err.msg));
        }
    });

    // delete snippet
    document.getElementById("delete-form").addEventListener('submit', e => {
        console.log(e);
        if (!confirm("Procedere con l'eliminazione?")) {
            console.log("Stop");
            e.preventDefault();
        } else {
            if (idSnippetSelected > 0) {
                document.getElementById("input-snippet").value = idSnippetSelected;
            } else {
                alert("Nessun elemento selezionato");
            }
        }
    });

    // copy clipboard
    document.getElementById("copy-clipboard").addEventListener('click', e => {
        var text = document.getElementById("snippet-content").value;
        navigator.clipboard.writeText(text);
    });

    addClickEventToSnippets();
    // imposto il primo snippet della lista come selezionato
    var firstSnippet = document.querySelector("#snippets-list .list-group-item");
    if (firstSnippet !== null) {
        getSnippet(firstSnippet.dataset.id);
    }

    // Aggiungi l'handler per l'evento di click sugli elementi della lista di snippets
    function addClickEventToSnippets() {
        document.querySelectorAll("#snippets-list .list-group-item").forEach(x => {
            x.addEventListener("click", e => {
                const id = e.target.closest("li").dataset.id;
                getSnippet(id);
            });
        });
    }

    // Imposta il background allo snippet selezionato e resetta gli altri
    function setSnippetSelected(id) {
        document.querySelectorAll("#snippets-list .list-group-item").forEach(x => {
            x.classList.remove("list-group-item-selected");
        });

        idSnippetSelected = id;

        document.querySelectorAll("#snippets-list .list-group-item").forEach(x => {
            if (x.dataset.id == idSnippetSelected) {
                x.classList.add("list-group-item-selected");
            }
        });
    }

    // chiamata al server per recuperare lo snippet selezionato e valorizzare i campi
    async function getSnippet(id) {
        http.get("/Home/GetSnippet?snippetId=" + id)
            .then(data => {
                if (data.status === 200) {
                    const snippet = data.snippet;
                    // aggiorno i campi del form
                    document.getElementById("snippet-title").value = snippet.title;
                    document.getElementById("snippet-description").value = snippet.description;
                    document.getElementById("snippet-content").value = snippet.content;
                    setSnippetSelected(id);
                }
                else {
                    alert(data.msg);
                }
            })
            .catch(err => alert(err.msg));
    }
});