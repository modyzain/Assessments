function getPatientBasicInfoData() {
    $('.data-table').DataTable({
        pageLength: 25,
        destroy: true,
        responsive: true,
        info: false,
        ajax: {
            method: "GET",
            url: "../GetPatienBasicInfo",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        "bProcessing": true,
        columns: [
            {
                sortable: false,
                "class": "auto-no text-center",
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "class": "text-left",
                "render": function (data, type, full) {
                    var foreName = full.foreName;
                    var surName = full.surName;

                    return '<span>' + foreName + ' ' + surName + '</span>';
                }
            },
            {
                "class": "text-center",
                "data": "pasNumber"
            },
            {
                "class": "text-center",
                "data": "gender"
            },
            {
                sortable: false,
                "class": "order text-center",
                "render": function (data, type, full) {
                    var id = full.pkPatientId;
                    var name = full.foreName + ' ' + full.surName;
                    return '<div class="btn-group"><button data-toggle="dropdown" class="btn btn-info btn-sm btn-outline dropdown-toggle">-- Select --</button>' +
                        '<ul class="dropdown-menu"><li><a class="dropdown-item text-info open-modal" href="../DetailsPatient?id=' + id + '" data-modal=""><i class="fa fa-info-circle icon-left"></i>Details</a></li>' +
                        '<li><a class="dropdown-item text-info open-modal" href="../EditPatient?id=' + id + '" data-modal=""><i class="fa fa-pencil icon-left"></i>Edit</a></li>' +
                        '<li><a class="dropdown-item text-danger open-modal" href="../RemovePatient?id=' + id + '" data-modal=""><i class="fa fa-trash icon-left"></i>Delete</a></li></ul ></div > ';
                }
            }
        ]
    });
};

function ManagePatientBasicInfo(url, check, id, gender, pasNumber, foreName, surName, dateOfBirth, homeTelephoneNumber, successMsg, errorMsg) {

    var formData = new FormData();
    formData.append("Check", check);
    formData.append("PkPatientId", id);
    formData.append("Gender", gender);
    formData.append("PasNumber", pasNumber);
    formData.append("ForeName", foreName);
    formData.append("SurName", surName);
    formData.append("DateOfBirth", dateOfBirth);
    formData.append("HomeTelephoneNumber", homeTelephoneNumber);


    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            if (response.ok) {
                toastr.success(successMsg, "Success");
            } else {
                toastr.error(errorMsg, "Error");
            }
        },
        failure: function (response) {
            toastr.error("Failure \n" + response.responseText, "Error");
        },
        error: function (xhr, status, error) {
            toastr.error("An AJAX error occured: " + status + "\nError: " + error + "\nError detail: " + xhr.responseText, "Error");
        }
    });
    $("#myModal").modal("hide");
    getPatientBasicInfoData();
};


function getNextOfKinRecordsData(id) {
    $('.kin-table').DataTable({
        pageLength: 25,
        destroy: true,
        responsive: true,
        info: false,
        ajax: {
            method: "GET",
            url: "../GetNextOfKin?id=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        "bProcessing": true,
        columns: [
            {
                "class": "text-left",
                "render": function (data, type, full) {
                    var nokName = full.nokName;

                    return '<span>' + nokName + '</span>';
                }
            },
            {
                "class": "text-center",
                "data": "nokRelationshipCode"
            },
            {
                "class": "text-center",
                "data": "nokAddressLine1"
            }
        ]
    });
};

function getNextOfKinData(id) {
    $('.data-table').DataTable({
        pageLength: 25,
        destroy: true,
        responsive: true,
        info: false,
        ajax: {
            method: "GET",
            url: "../GetNextOfKin?id=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        "bProcessing": true,
        columns: [
            {
                sortable: false,
                "class": "auto-no text-center",
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "class": "text-left",
                "render": function (data, type, full) {
                    var nokName = full.nokName;

                    return '<span>' + nokName + '</span>';
                }
            },
            {
                "class": "text-center",
                "data": "nokRelationshipCode"
            },
            {
                "class": "text-center",
                "data": "nokAddressLine1"
            },
            {
                sortable: false,
                "class": "order text-center",
                "render": function (data, type, full) {
                    var id = full.pkNokId;
                    return '<div class="btn-group"><button data-toggle="dropdown" class="btn btn-info btn-sm btn-outline dropdown-toggle">-- Select --</button>' +
                        '<ul class="dropdown-menu"><li><a class="dropdown-item text-info open-modal" href="../EditNextOfKin?id=' + id + '" data-modal=""><i class="fa fa-pencil icon-left"></i>Edit</a></li><li><a class="dropdown-item text-danger open-modal" href="../RemoveNextOfKin?id=' + id + '" data-modal=""><i class="fa fa-trash icon-left"></i>Delete</a></li>';
                }
            }
        ]
    });
};

function ManageNextOfKin(url, check, id, fkPatientId, fkNokRelationshipId, nokName, nokAddressLine1, nokAddressLine2, nokAddressLine3, nokAddressLine4, nokPostcode, successMsg, errorMsg) {

    var formData = new FormData();
    formData.append("Check", check);
    formData.append("PkNokId", id);
    formData.append("FkPatientId", fkPatientId);
    formData.append("FkNokRelationshipId", fkNokRelationshipId);
    formData.append("NokName", nokName);
    formData.append("NokAddressLine1", nokAddressLine1);
    formData.append("NokAddressLine2", nokAddressLine2);
    formData.append("NokAddressLine3", nokAddressLine3);
    formData.append("NokAddressLine4", nokAddressLine4);
    formData.append("NokPostcode", nokPostcode);


    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            if (response.ok) {
                toastr.success(successMsg, "Success");
            } else {
                toastr.error(errorMsg, "Error");
            }
        },
        failure: function (response) {
            toastr.error("Failure \n" + response.responseText, "Error");
        },
        error: function (xhr, status, error) {
            toastr.error("An AJAX error occured: " + status + "\nError: " + error + "\nError detail: " + xhr.responseText, "Error");
        }
    });
    $("#myModal").modal("hide");
    getNextOfKinData(fkPatientId);
};

function getPatientNextOfKinData() {
    $('.data-table').DataTable({
        pageLength: 25,
        destroy: true,
        responsive: true,
        info: false,
        ajax: {
            method: "GET",
            url: "../GetPatienBasicInfo",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        "bProcessing": true,
        columns: [
            {
                sortable: false,
                "class": "auto-no text-center",
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "class": "text-left",
                "render": function (data, type, full) {
                    var foreName = full.foreName;
                    var surName = full.surName;

                    return '<span>' + foreName + ' ' + surName + '</span>';
                }
            },
            {
                "class": "text-center",
                "data": "pasNumber"
            },
            {
                "class": "text-center",
                "data": "gender"
            },
            {
                sortable: false,
                "class": "order text-center",
                "render": function (data, type, full) {
                    var id = full.pkPatientId;
                    var name = full.foreName + ' ' + full.surName;
                    return '<a class="btn btn-sm btn-info" href="../NextOfKin/NextOfKinIndex?id=' + id + '&name=' + name + '">NextOfKin</a>';
                }
            }
        ]
    });
};


function getGpDetailsRecordsData(id) {
    $('.dp-table').DataTable({
        pageLength: 25,
        destroy: true,
        responsive: true,
        info: false,
        ajax: {
            method: "GET",
            url: "../GetGPDetails?id=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        "bProcessing": true,
        columns: [
            {
                "class": "text-left",
                "render": function (data, type, full) {
                    var surname = full.gpSurname;

                    return '<span>' + surname + '</span>';
                }
            },
            {
                "class": "text-center",
                "data": "gpCode"
            },
            {
                "class": "text-center",
                "data": "gpPhone"
            }
        ]
    });
};

function getGpDetailsData(id) {
    $('.data-table').DataTable({
        pageLength: 25,
        destroy: true,
        responsive: true,
        info: false,
        ajax: {
            method: "GET",
            url: "../GetGPDetails?id=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        "bProcessing": true,
        columns: [
            {
                sortable: false,
                "class": "auto-no text-center",
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "class": "text-left",
                "render": function (data, type, full) {
                    var surname = full.gpSurname;

                    return '<span>' + surname + '</span>';
                }
            },
            {
                "class": "text-center",
                "data": "gpCode"
            },
            {
                "class": "text-center",
                "data": "gpPhone"
            },
            {
                sortable: false,
                "class": "order text-center",
                "render": function (data, type, full) {
                    var id = full.pkGpDetailsId;
                    return '<div class="btn-group"><button data-toggle="dropdown" class="btn btn-info btn-sm btn-outline dropdown-toggle">-- Select --</button>' +
                        '<ul class="dropdown-menu"><li><a class="dropdown-item text-info open-modal" href="../EditGPDetails?id=' + id + '" data-modal=""><i class="fa fa-pencil icon-left"></i>Edit</a></li><li><a class="dropdown-item text-danger open-modal" href="../RemoveGpDetails?id=' + id + '" data-modal=""><i class="fa fa-trash icon-left"></i>Delete</a></li>';
                }
            }
        ]
    });
};

function ManageGpDetails(url, check, id, fkPatientId, gpCode, gpSurname, gpInitials, gpPhone, successMsg, errorMsg) {

    var formData = new FormData();
    formData.append("Check", check);
    formData.append("PkGpDetailsId", id);
    formData.append("FkPatientId", fkPatientId);
    formData.append("GpCode", gpCode);
    formData.append("GpSurname", gpSurname);
    formData.append("GpInitials", gpInitials);
    formData.append("GpPhone", gpPhone);


    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: formData,
        success: function (response) {
            if (response.ok) {
                toastr.success(successMsg, "Success");
            } else {
                toastr.error(errorMsg, "Error");
            }
        },
        failure: function (response) {
            toastr.error("Failure \n" + response.responseText, "Error");
        },
        error: function (xhr, status, error) {
            toastr.error("An AJAX error occured: " + status + "\nError: " + error + "\nError detail: " + xhr.responseText, "Error");
        }
    });
    $("#myModal").modal("hide");
    getGpDetailsData(fkPatientId);
};

function getPatientGpDetailsData() {
    $('.data-table').DataTable({
        pageLength: 25,
        destroy: true,
        responsive: true,
        info: false,
        ajax: {
            method: "GET",
            url: "../GetPatienBasicInfo",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: function (data) {
                return JSON.stringify(data);
            }
        },
        "bProcessing": true,
        columns: [
            {
                sortable: false,
                "class": "auto-no text-center",
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                "class": "text-left",
                "render": function (data, type, full) {
                    var foreName = full.foreName;
                    var surName = full.surName;

                    return '<span>' + foreName + ' ' + surName + '</span>';
                }
            },
            {
                "class": "text-center",
                "data": "pasNumber"
            },
            {
                "class": "text-center",
                "data": "gender"
            },
            {
                sortable: false,
                "class": "order text-center",
                "render": function (data, type, full) {
                    var id = full.pkPatientId;
                    var name = full.foreName + ' ' + full.surName;
                    return '<a class="btn btn-sm btn-info" href="../Gpdetails/GpDetailsIndex?id=' + id + '&name=' + name + '">Gp Details</a>';
                }
            }
        ]
    });
};
