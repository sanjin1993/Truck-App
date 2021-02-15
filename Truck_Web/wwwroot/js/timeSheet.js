
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/timeSheets/GetAllTimeSheet",
            "type": "GET",
            "datatype": "json",
        },
        "columns": [
            { "data": "id", "width": "7,5%" },
            { "data": "type", "width": "7,5%" },
            { "data": "startTime", "width": "7,5%" },        
            { "data": "endTime", "width": "7,5%" },        
            { "data": "break", "width": "7,5%" },
            {
                "data": null,
                "render": function(data, type, row)  {
                    var time1 = row.startTime.split(':'), time2 = row.endTime.split(':');
                    var hours1 = parseInt(time1[0], 10),
                    hours2 = parseInt(time2[0], 10),
                    mins1 = parseInt(time1[1], 10),
                    mins2 = parseInt(time2[1], 10);
                    var hours = hours2 - hours1, mins = 0;

                    // get hours
                    if(hours < 0) hours = 24 + hours;

                    // get minutes
                    if(mins2 >= mins1) {
                        mins = mins2 - mins1;
                    }
                    else {
                        mins = (mins2 + 60) - mins1;
                        hours--;
                    }

                    // convert to fraction of 60
                    mins = mins / 60;

                    hours += mins;
                    hours = hours.toFixed(2);
                    return hours ;
                }
                    , "width": "7,5%"
            },        
            { "data": "m", "width": "7,5%" },        
            { "data": "kmStand", "width": "7,5%" },        
            { "data": "privat", "width": "7,5%" },        
            { "data": "fuel", "width": "7,5%" },        
            { "data": "adBlue", "width": "7,5%" },        
            { "data": "notes", "width": "7,5%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/timeSheets/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer;'><i class='far fa-edit'></i></a>
                                &nbsp;  
                                <a onclick=Delete("/timeSheets/Delete/${data}") class='btn btn-danger text-white' style='cursor:pointer;'><i class='far fa-trash-alt'></i></a>
                            </div>
                            `
                }, "width":"10%"
            }

        ]
    });
}


//function calculate() {
//    var time1 = data[2].startTime.val.split(':'), time2 = data[3].endTime.val.split(':');
//    var hours1 = parseInt(time1[0], 10),
//        hours2 = parseInt(time2[0], 10),
//        mins1 = parseInt(time1[1], 10),
//        mins2 = parseInt(time2[1], 10);
//    var hours = hours2 - hours1, mins = 0;

//    // get hours
//    if (hours < 0) hours = 24 + hours;

//    // get minutes
//    if (mins2 >= mins1) {
//        mins = mins2 - mins1;
//    }
//    else {
//        mins = (mins2 + 60) - mins1;
//        hours--;
//    }

//    // convert to fraction of 60
//    mins = mins / 60;

//    hours += mins;
//    hours = hours.toFixed(2);
//    $(data[5].endTime).val(hours);
//}





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