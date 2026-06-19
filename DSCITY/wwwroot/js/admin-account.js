let accountPageState;

function getInitials(fullName) {
  return (fullName || 'LH')
    .trim()
    .split(/\s+/)
    .slice(0, 2)
    .map((part) => part.charAt(0).toUpperCase())
    .join('');
}

function renderProfile() {
  const { els, profile } = accountPageState;
  els.avatar.textContent = getInitials(profile.fullName);
  els.fullName.textContent = profile.fullName;
  els.email.textContent = profile.email;
  els.role.textContent = profile.role;
  els.roleSecondary.textContent = profile.role;
  els.createdAt.textContent = profile.createdAt;
  els.status.textContent = profile.status;
  els.statusSecondary.textContent = profile.status;
}

function openModal() {
  const { els, profile } = accountPageState;
  els.editFullName.value = profile.fullName;
  els.editEmail.value = profile.email;
  els.editRole.value = profile.role;
  els.editCreatedAt.value = profile.createdAt;
  els.editStatus.value = profile.status;
  els.modal.hidden = false;
  els.modal.classList.add('show');
}

function closeModal() {
  const { els } = accountPageState;
  els.modal.classList.remove('show');
  els.modal.hidden = true;
}

function saveProfile() {
  const { els, labels } = accountPageState;
  const fullName = els.editFullName.value.trim();
  const email = els.editEmail.value.trim();
  const role = els.editRole.value.trim();
  const createdAt = els.editCreatedAt.value.trim();
  const status = els.editStatus.value;

  if (!fullName || !email || !role || !createdAt) {
    window.alert(labels.alertIncomplete || 'Please complete all account profile fields.');
    return;
  }

  accountPageState.profile = { fullName, email, role, createdAt, status };
  renderProfile();
  closeModal();
}

export function initAdminAccountPage(config = {}) {
  const root = document.getElementById('adminAccountPage');
  if (!root) {
    return;
  }

  if (!accountPageState) {
    accountPageState = {
      root,
      els: {
        avatar: root.querySelector('#profileAvatar'),
        fullName: root.querySelector('#profileFullName'),
        email: root.querySelector('#profileEmail'),
        role: root.querySelector('#profileRole'),
        roleSecondary: root.querySelector('#profileRoleSecondary'),
        createdAt: root.querySelector('#profileCreatedAt'),
        status: root.querySelector('#profileStatus'),
        statusSecondary: root.querySelector('#profileStatusSecondary'),
        editButton: root.querySelector('#editProfileButton'),
        modal: root.querySelector('#profileModalBackdrop'),
        closeModal: root.querySelector('#closeProfileModal'),
        cancelModal: root.querySelector('#cancelProfileEdit'),
        saveModal: root.querySelector('#saveProfileEdit'),
        editFullName: root.querySelector('#editFullName'),
        editEmail: root.querySelector('#editEmail'),
        editRole: root.querySelector('#editRole'),
        editCreatedAt: root.querySelector('#editCreatedAt'),
        editStatus: root.querySelector('#editStatus')
      },
      profile: {
        fullName: config.fullName || 'Lê Thu Hà',
        email: config.email || 'lethuha@dscity.vn',
        role: config.role || 'Quản trị viên cấp cao',
        createdAt: config.createdAt || '08/06/2026',
        status: config.status || 'Đang hoạt động'
      },
      labels: config.labels || {}
    };

    const elements = accountPageState.els;
    elements.editButton?.addEventListener('click', openModal);
    elements.closeModal?.addEventListener('click', closeModal);
    elements.cancelModal?.addEventListener('click', closeModal);
    elements.saveModal?.addEventListener('click', saveProfile);
    elements.modal?.addEventListener('click', (event) => {
      if (event.target === elements.modal) {
        closeModal();
      }
    });
  }

  accountPageState.profile = {
    fullName: config.fullName || accountPageState.profile.fullName,
    email: config.email || accountPageState.profile.email,
    role: config.role || accountPageState.profile.role,
    createdAt: config.createdAt || accountPageState.profile.createdAt,
    status: config.status || accountPageState.profile.status
  };
  accountPageState.labels = config.labels || accountPageState.labels;

  renderProfile();
}
