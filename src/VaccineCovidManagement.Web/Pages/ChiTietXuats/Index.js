
$(function () {
    var l = abp.localization.getResource('VaccineCovidManagement');

    var createModal = new abp.ModalManager(abp.appPath + 'ChiTietXuats/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'ChiTietXuats/EditModal');

    var datatable = $('#XuatVaccineTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(vaccineCovidManagement.chiTietXuats.chiTietXuat.getList),
            columnDefs: [
                {
                    title: l("STT"),
                    data: "stt"
                },
                {
                    title: l("Tên Đơn vị Y tế"),
                    data: "tenDonViYTe"
                },
                {
                    title: l("Tên Vaccine xuất"),
                    data: "tenVaccineXuat"
                },
                {
                    title: l("Số lượng xuất"),
                    data: "soLuongXuat"
                },
                {
                    title: l('Thời gian xuất'), data: "creationTime",
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
                                    visible: abp.auth.isGranted('VaccineCovidManagement.ChiTietXuats.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Xóa'),
                                    iconClass: "fa fa-trash-o",
                                    visible: abp.auth.isGranted('VaccineCovidManagement.ChiTietXuats.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'Thông báo Xác nhận xóa Vaccine xuất',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        vaccineCovidManagement.chiTietXuats.chiTietXuat
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

    $('#XuatVaccine').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

})