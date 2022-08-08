
$(function () {
    var l = abp.localization.getResource('VaccineCovidManagement');

    var createModal = new abp.ModalManager(abp.appPath + 'DonViYTes/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'DonViYTes/EditModal');

    var datatable = $('#DonViYTeTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(vaccineCovidManagement.donViYTes.donViYTe.getList),
            columnDefs: [
                {
                    title: l("STT"),
                    data: 'stt'
                },
                {
                    title: l("Tơn vị y tế"),
                    data: "tenDonViYTe"
                },
                {
                    title: l("Địa Chỉ"),
                    data: "diaChi"
                },
                {
                    title: l("Số điện thoại"),
                    data: "sdt"
                },
                {
                    title: l('Thời gian tạo'), data: "creationTime",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                },
                {
                    title: l('Tác vụ'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Sửa'),
                                    iconClass: "fa fa-pencil-square-o",
                                    visible: abp.auth.isGranted('VaccineCovidManagement.DonViYTes.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Xóa'),
                                    iconClass: "fa fa-trash-o",
                                    visible: abp.auth.isGranted('VaccineCovidManagement.DonViYTes.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'Thông báo Xác nhận xóa Đơn vị Y tế',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        vaccineCovidManagement.donViYTes.donViYTe
                                            .delete(data.record.id)
                                            .then(function (data) {
                                                if (data) {
                                                    abp.notify.info(l('Xóa thành công'));
                                                    datatable.ajax.reload();
                                                } else {
                                                    abp.message.error(l("Xóa thất bại"));
                                                }
                                            });
                                    }
                                }
                            ]
                    }
                }
            ]
        })
    );

    createModal.onResult(function () {
        datatable.ajax.reload();
    });

    editModal.onResult(function () {
        datatable.ajax.reload();
    });

    $('#DonViYTeButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
})