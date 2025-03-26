window.showModal = (modalId) => {
    var modalElement = new bootstrap.Modal(document.getElementById(modalId));
    modalElement.show();
};

window.hideModal = (modalId) => {
    var modalElement = new bootstrap.Modal(document.getElementById(modalId));
    modalElement.hide();
};
