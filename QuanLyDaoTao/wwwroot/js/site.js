// Khởi tạo DataTables nếu chưa tồn tại
$(document).ready(function () {
    $('.datatable').each(function () {
        if (!$.fn.DataTable.isDataTable(this)) {
            $(this).DataTable({
                language: {
                    url: '//cdn.datatables.net/plug-ins/1.11.5/i18n/vi.json'
                }
            });
        }
    });
});

// Submit form bằng AJAX an toàn
function submitFormAjax(formId, successCallback) {
    $(document).off('submit', formId).on('submit', formId, function (e) {
        e.preventDefault();
        const form = $(this);
        const url = form.attr('action');

        if (!form[0].checkValidity()) {
            form[0].reportValidity();
            return;
        }

        $.ajax({
            type: "POST",
            url: url,
            data: form.serialize(),
            success: function (data) {
                if (successCallback) successCallback(data);
                showNotification('Gửi thành công!');
            },
            error: function (xhr, status, error) {
                showNotification('Có lỗi xảy ra: ' + error, 'danger');
            }
        });
    });
}

// Hiển thị thông báo với Bootstrap alert
function showNotification(message, type = 'success') {
    const alertClass = type === 'success' ? 'alert-success' : 'alert-danger';
    const alert = $(`
        <div class="alert ${alertClass} alert-dismissible fade show" role="alert">
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `);

    $('#notification-container').append(alert);

    setTimeout(() => {
        alert.alert('close');
    }, 5000);
}

// Xác nhận trước khi xóa
function confirmDelete(message = 'Bạn có chắc chắn muốn xóa?') {
    return confirm(message);
}

// Format ngày tháng theo chuẩn Việt Nam
function formatDate(date) {
    if (!date) return '';
    const d = new Date(date);
    return d.toLocaleDateString('vi-VN');
}

// Kiểm tra form HTML5
function validateForm(formId) {
    const form = $(formId);
    if (!form[0].checkValidity()) {
        form[0].reportValidity();
        return false;
    }
    return true;
}
