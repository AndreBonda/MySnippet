window.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll("textarea.avoid-newline").forEach(x => {
        x.addEventListener("keydown", e => {
            if (e.key == 'Enter') {
                e.preventDefault();
            }
        });
    });
});