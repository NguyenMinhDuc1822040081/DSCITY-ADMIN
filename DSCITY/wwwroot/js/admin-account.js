const sampleHistory = [
  {
    id: 1,
    timestamp: '2026-06-11 09:20',
    action: 'Sửa',
    target: 'Hồ sơ doanh nghiệp Skyline Mobility',
    before: 'Người phụ trách: Nguyễn Hoàng Anh',
    after: 'Người phụ trách: Lê Thu Hà'
  },
  {
    id: 2,
    timestamp: '2026-06-11 08:45',
    action: 'Thêm',
    target: 'Hợp đồng Central Plaza Parking',
    before: 'Chưa có phụ lục thanh toán',
    after: 'Đã thêm phụ lục thanh toán kỳ tháng 06'
  },
  {
    id: 3,
    timestamp: '2026-06-10 16:35',
    action: 'Xóa',
    target: 'Thông báo nhắc việc quá hạn',
    before: 'Nhắc việc gia hạn hợp đồng cũ',
    after: 'Đã xóa thông báo trùng lặp'
  },
  {
    id: 4,
    timestamp: '2026-06-10 14:10',
    action: 'Sửa',
    target: 'Hồ sơ đăng ký REG-260608-2113',
    before: 'Trạng thái: Chờ bổ sung',
    after: 'Trạng thái: Chờ phê duyệt nội bộ'
  },
  {
    id: 5,
    timestamp: '2026-06-09 17:20',
    action: 'Sửa',
    target: 'Hồ sơ doanh nghiệp Urban Charge Hub',
    before: 'Email liên hệ: contact@urban.vn',
    after: 'Email liên hệ: partner@urban.vn'
  },
  {
    id: 6,
    timestamp: '2026-06-09 10:02',
    action: 'Thêm',
    target: 'Người dùng quản trị nội bộ',
    before: 'Chưa có tài khoản',
    after: 'Tạo mới tài khoản pháp chế nội bộ'
  },
  {
    id: 7,
    timestamp: '2026-06-08 15:30',
    action: 'Sửa',
    target: 'Hợp đồng BikeGo Campus',
    before: 'Điều khoản SLA: 24 giờ',
    after: 'Điều khoản SLA: 12 giờ'
  },
  {
    id: 8,
    timestamp: '2026-06-08 11:12',
    action: 'Xóa',
    target: 'Hồ sơ thử nghiệm nội bộ',
    before: 'Hồ sơ chưa kích hoạt',
    after: 'Đã xóa hồ sơ test'
  },
  {
    id: 9,
    timestamp: '2026-06-07 18:40',
    action: 'Thêm',
    target: 'Bản ghi lịch sử thanh toán',
    before: 'Chưa có chứng từ',
    after: 'Đã tải lên hóa đơn điện tử'
  }
];

function getInitials(fullName) {
  return (fullName || 'LH')
    .trim()
    .split(/\s+/)
    .slice(0, 2)
    .map((part) => part.charAt(0).toUpperCase())
    .join('');
}

function dateKey(timestamp) {
  return String(timestamp).slice(0, 10);
}

function formatNow() {
  const now = new Date();
  const yyyy = now.getFullYear();
  const mm = String(now.getMonth() + 1).padStart(2, '0');
  const dd = String(now.getDate()).padStart(2, '0');
  const hh = String(now.getHours()).padStart(2, '0');
  const mi = String(now.getMinutes()).padStart(2, '0');
  return `${yyyy}-${mm}-${dd} ${hh}:${mi}`;
}

function escapeHtml(value) {
  return String(value ?? '')
    .replaceAll('&', '&amp;')
    .replaceAll('<', '&lt;')
    .replaceAll('>', '&gt;')
    .replaceAll('"', '&quot;')
    .replaceAll("'", '&#39;');
}

function actionClass(action) {
  switch (action) {
    case 'Thêm': return 'success';
    case 'Xóa': return 'danger';
    default: return 'info';
  }
}

export function initAdminAccountPage(config = {}) {
  const root = document.getElementById('adminAccountPage');
  if (!root || root.dataset.initialized === 'true') {
    return;
  }

  root.dataset.initialized = 'true';

  const profile = {
    fullName: config.fullName || 'Lê Thu Hà',
    email: config.email || 'lethuha@dscity.vn',
    role: config.role || 'Quản trị viên cấp cao',
    createdAt: config.createdAt || '08/06/2026',
    status: config.status || 'Đang hoạt động'
  };

  const state = {
    page: 1,
    pageSize: 5,
    profile,
    history: [...sampleHistory]
  };

  const els = {
    avatar: root.querySelector('#profileAvatar'),
    fullName: root.querySelector('#profileFullName'),
    email: root.querySelector('#profileEmail'),
    role: root.querySelector('#profileRole'),
    roleSecondary: root.querySelector('#profileRoleSecondary'),
    createdAt: root.querySelector('#profileCreatedAt'),
    status: root.querySelector('#profileStatus'),
    statusSecondary: root.querySelector('#profileStatusSecondary'),
    editButton: root.querySelector('#editProfileButton'),
    tableBody: root.querySelector('#historyTableBody'),
    pageInfo: root.querySelector('#historyPageInfo'),
    prevPage: root.querySelector('#historyPrevPage'),
    nextPage: root.querySelector('#historyNextPage'),
    fromDate: root.querySelector('#historyDateFrom'),
    toDate: root.querySelector('#historyDateTo'),
    resetFilters: root.querySelector('#resetHistoryFilters'),
    modal: root.querySelector('#profileModalBackdrop'),
    closeModal: root.querySelector('#closeProfileModal'),
    cancelModal: root.querySelector('#cancelProfileEdit'),
    saveModal: root.querySelector('#saveProfileEdit'),
    editFullName: root.querySelector('#editFullName'),
    editEmail: root.querySelector('#editEmail'),
    editRole: root.querySelector('#editRole'),
    editCreatedAt: root.querySelector('#editCreatedAt'),
    editStatus: root.querySelector('#editStatus')
  };

  function renderProfile() {
    els.avatar.textContent = getInitials(state.profile.fullName);
    els.fullName.textContent = state.profile.fullName;
    els.email.textContent = state.profile.email;
    els.role.textContent = state.profile.role;
    els.roleSecondary.textContent = state.profile.role;
    els.createdAt.textContent = state.profile.createdAt;
    els.status.textContent = state.profile.status;
    els.statusSecondary.textContent = state.profile.status;
  }

  function getFilteredHistory() {
    return state.history.filter((item) => {
      const itemDate = dateKey(item.timestamp);
      const from = els.fromDate.value || '';
      const to = els.toDate.value || '';
      if (from && itemDate < from) {
        return false;
      }
      if (to && itemDate > to) {
        return false;
      }
      return true;
    });
  }

  function renderTable() {
    const filtered = getFilteredHistory();
    const totalPages = Math.max(1, Math.ceil(filtered.length / state.pageSize));
    state.page = Math.min(state.page, totalPages);
    const start = (state.page - 1) * state.pageSize;
    const pageItems = filtered.slice(start, start + state.pageSize);

    if (!pageItems.length) {
      els.tableBody.innerHTML = '<tr><td colspan="5" class="audit-empty">Không có bản ghi nào phù hợp với bộ lọc hiện tại.</td></tr>';
    } else {
      els.tableBody.innerHTML = pageItems.map((item) => `
        <tr>
          <td data-label="Thời gian">${escapeHtml(item.timestamp)}</td>
          <td data-label="Hành động"><span class="audit-action ${actionClass(item.action)}">${escapeHtml(item.action)}</span></td>
          <td data-label="Đối tượng bị tác động">${escapeHtml(item.target)}</td>
          <td data-label="Giá trị cũ">${escapeHtml(item.before)}</td>
          <td data-label="Giá trị mới">${escapeHtml(item.after)}</td>
        </tr>
      `).join('');
    }

    els.pageInfo.textContent = `Trang ${state.page} / ${totalPages}`;
    els.prevPage.disabled = state.page <= 1;
    els.nextPage.disabled = state.page >= totalPages;
  }

  function openModal() {
    els.editFullName.value = state.profile.fullName;
    els.editEmail.value = state.profile.email;
    els.editRole.value = state.profile.role;
    els.editCreatedAt.value = state.profile.createdAt;
    els.editStatus.value = state.profile.status;
    els.modal.hidden = false;
    els.modal.classList.add('show');
  }

  function closeModal() {
    els.modal.classList.remove('show');
    els.modal.hidden = true;
  }

  function saveProfile() {
    const oldProfile = { ...state.profile };
    const fullName = els.editFullName.value.trim();
    const email = els.editEmail.value.trim();
    const role = els.editRole.value.trim();
    const createdAt = els.editCreatedAt.value.trim();
    const status = els.editStatus.value;

    if (!fullName || !email || !role || !createdAt) {
      window.alert('Vui lòng nhập đầy đủ thông tin hồ sơ tài khoản.');
      return;
    }

    state.profile = { fullName, email, role, createdAt, status };
    const before = `Họ tên: ${oldProfile.fullName} | Email: ${oldProfile.email} | Vai trò: ${oldProfile.role} | Trạng thái: ${oldProfile.status}`;
    const after = `Họ tên: ${state.profile.fullName} | Email: ${state.profile.email} | Vai trò: ${state.profile.role} | Trạng thái: ${state.profile.status}`;

    state.history.unshift({
      id: Date.now(),
      timestamp: formatNow(),
      action: 'Sửa',
      target: 'Hồ sơ tài khoản quản trị viên',
      before,
      after
    });

    state.page = 1;
    renderProfile();
    renderTable();
    closeModal();
  }

  els.editButton.addEventListener('click', openModal);
  els.closeModal.addEventListener('click', closeModal);
  els.cancelModal.addEventListener('click', closeModal);
  els.modal.addEventListener('click', (event) => {
    if (event.target === els.modal) {
      closeModal();
    }
  });
  els.saveModal.addEventListener('click', saveProfile);
  els.fromDate.addEventListener('change', () => {
    state.page = 1;
    renderTable();
  });
  els.toDate.addEventListener('change', () => {
    state.page = 1;
    renderTable();
  });
  els.resetFilters.addEventListener('click', () => {
    els.fromDate.value = '';
    els.toDate.value = '';
    state.page = 1;
    renderTable();
  });
  els.prevPage.addEventListener('click', () => {
    if (state.page > 1) {
      state.page -= 1;
      renderTable();
    }
  });
  els.nextPage.addEventListener('click', () => {
    const totalPages = Math.max(1, Math.ceil(getFilteredHistory().length / state.pageSize));
    if (state.page < totalPages) {
      state.page += 1;
      renderTable();
    }
  });

  renderProfile();
  renderTable();
}
