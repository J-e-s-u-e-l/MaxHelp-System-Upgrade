// Notification Panel Toggle
const notificationPanel = document.getElementById("notificationPanel");
const notificationBell = document.getElementById("notificationBell");

// Function to toggle panel visibility
function openNotificationPanel() {
    notificationPanel.classList.add("active");
}

function closeNotificationPanel() {
    notificationPanel.classList.remove("active");
}

// Attach event listener to notification bell to open the panel
notificationBell.addEventListener("click", openNotificationPanel);

// Attach event listener to close button
function attachCloseEventListener() {
    const closeButton = document.getElementById("closeNotification");
    if (closeButton) {
        closeButton.addEventListener("click", closeNotificationPanel);
    }
}

// AJAX request to load notifications
document.addEventListener("DOMContentLoaded", function () {
    const notificationPanel = document.getElementById("notificationPanel");
    const notificationBadge = document.getElementById("notificationBadge");

    fetch('https://localhost:7171/Notification/GetNotifications')
        .then(response => response.json())
        .then(data => {
            console.log('Notifications:', data);

            // Hide the loading spinner
            document.getElementById("loadingSpinner").style.display = "none";

            // Update the badge count
            const notificationCount = data.length;
            if (notificationCount > 0) {
                notificationBadge.textContent = notificationCount;
                notificationBadge.style.display = 'inline';
            } else {
                notificationBadge.style.display = 'none';
            }

            // Clear existing content inside the notification panel
            notificationPanel.innerHTML = `
                        <div class="notification-header">
                            <h5>Notifications</h5>
                            <button id="closeNotification" class="btn btn-sm btn-outline-danger">X</button>
                        </div>
                    `;

            // Populate the notification panel with data
            data.forEach(notification => {
                const notificationItem = document.createElement('div');
                notificationItem.className = 'notification-item';
                notificationItem.innerHTML = `
                            <h3>${notification.businessUnitName}</h3>
                            <p>${notification.itemName} is almost out of stock.</p>
                        `;
                notificationPanel.appendChild(notificationItem);
            });

            // Rebind the close button functionality
            document.getElementById("closeNotification").addEventListener("click", () => {
                notificationPanel.classList.remove("active");
            });
        })
        .catch(error => {
            console.error('Error fetching notifications:', error);
            document.getElementById("loadingSpinner").style.display = "none";
            notificationPanel.innerHTML = `<p>Error loading notifications. Please try again later.</p>`;
        });
});

// Sidebar Toggle
const sidebar = document.getElementById("sidebar");
const mainContent = document.getElementById("mainContent");
const closeSidebarBtn = document.querySelector(".close-sidebar");
document.getElementById("sidebarToggle").addEventListener("click", () => {
    sidebar.classList.toggle("active");
    mainContent.classList.toggle("shifted");
});
closeSidebarBtn.addEventListener("click", () => {
    sidebar.classList.remove("active");
    mainContent.classList.remove("shifted");
});