window.ShowToastr = (type, message) =>
{
    if (type == "success")
    {
        toastr.success(message, "Successful", { timeout: 5000 });
    }
    if (type == "error")
    {
        toastr.error(message, "Failed", { timeout: 5000 });
    }

}

function ShowDeleteModal()
{
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteModal()
{
    $('#deleteConfirmationModal').modal('hide');
}
