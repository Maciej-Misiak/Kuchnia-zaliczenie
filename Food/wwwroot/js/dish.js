var dataTable;

$(document).ready(function () {
    loadDataTable();
});

    function loadDataTable() {
        dataTable = $('#tblData').DataTable({
            "ajax": {
                "url":"/Admin/Dish/GetAll"
            },
            "columns": [
                { "data": "name", "width": "15%" },
                { "data": "ingredients", "width": "15%" },
                { "data": "preparation", "width": "15%" },
                { "data": "price", "width": "15%" },
                { "data": "category.name", "width": "15%" },
                { "data": "levelType.name", "width": "15%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            <div class="w-50 btn-group" role = "group">
                            <a href="/Admin/Dish/Upsert?id=${data}"
                            class="btn btn-primary mx-2 justify" > Edytuj</a >
                            <a onClick=Delete('/Admin/Dish/Delete/${data}')
                            class="btn btn-danger mx-2">Usuń</a>
                            </div >
                            `                           
                    },
                    "width": "15%"
                },
            ]
        });
}

function Delete(url) {
    Swal.fire({
        title: 'Jesteś pewny?',
        text: "Nie będzie możliwości powrotu!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Tak!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'Delete',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}