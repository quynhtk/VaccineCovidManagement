
$(function () {
    var l = abp.localization.getResource('VaccineCovidManagement');

    var createModal = new abp.ModalManager(abp.appPath + 'ChiTietNhaps/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'ChiTietNhaps/EditModal');

    var datatable = $('#ChiTietNhapTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(vaccineCovidManagement.chiTietNhaps.chiTietNhap.getList),
            columnDefs: [
                {
                    title: l("STT"),
                    data: 'stt'
                },
                {
                    title: l("Nhà sản xuất"),
                    data: "tenNhaSX"
                },
                {
                    title: l("Tên Vaccine nhập"),
                    data: "tenVaccineNhap"
                },
                {
                    title: l("Ngày sản xuất"),
                    data: "ngaySx"
                },
                {
                    title: l("Hạn sử dụng"),
                    data: "hanSuDung"
                },
                {
                    title: l("Số lượng nhập"),
                    data: "soLuongNhap"
                },
                {
                    title: l('Thời gian nhập'), data: "creationTime",
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
                                    visible: abp.auth.isGranted('VaccineCovidManagement.ChiTietNhaps.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Xóa'),
                                    iconClass: "fa fa-trash-o",
                                    visible: abp.auth.isGranted('VaccineCovidManagement.ChiTietNhaps.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'Thông báo Xác nhận xóa Vaccine nhập',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        vaccineCovidManagement.chiTietNhaps.chiTietNhap
                                            .delete(data.record.id)
                                            .then(function (data) {
                                                if (data) {
                                                    abp.notify.info(l('Xóa thành công'));
                                                    datatable.ajax.reload();
                                                } else {
                                                    abp.message.error(l("Xóa thất bại, Vaccine đã được nhập vào kho"));
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

    $('#ChiTietNhapButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
})