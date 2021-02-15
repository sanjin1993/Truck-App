﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/auditFiles/GetAllAuditFile",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "dayNo", "width": "20%" },
            { "data": "fieldEnum", "width": "20%" },
            { "data": "timeSheet.startTime", "width": "20%" },
            { "data": "newValue", "width": "20%" },
          
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/auditFiles/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer;'><i class='far fa-edit'></i></a>
                                &nbsp;  
                                <a onclick=Delete("/auditFiles/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer;'><i class='far fa-trash-alt'></i></a>
                            </div>
                            `
                }, "width": "20%"
            }

        ]
    });
}



function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: 'DELETE',
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}