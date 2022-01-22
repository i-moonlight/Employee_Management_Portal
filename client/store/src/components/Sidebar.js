import React from 'react';
import { Link } from 'react-router-dom';
import { Menu } from 'primereact/menu';
import { useAuth } from '../auth/useAuth';

const Sidebar = () => {
    const { signout, user } = useAuth();

    const items = [
        {
            label: "Dashboard",
            icon: "pi pi-th-large",
            template: (item, options) => {
                return (
                    <Link to="/admin/dashboard" className={options.className}>
                        <span className={options.iconClassName}></span>
                        <span className={options.labelClassName}>{item.label}</span>
                    </Link>
                )
            }
        },
        {
            label: "Cart",
            icon: "pi pi-shopping-cart",
            template: (item, options) => {
                return (
                    <Link to="/admin/cart" className={options.className}>
                        <span className={options.iconClassName}></span>
                        <span className={options.labelClassName}>{item.label}</span>
                    </Link>
                )
            }
        },
        {
            label: "Sign Out",
            icon: "pi pi-sign-out",
            command: () => signout()
        }
    ]

    return (
        <div className="sidebar">
            <h3>
                Sidebar
            </h3>
            <Menu model={items} className={"sidebar"}/>
        </div>
    )
}

export default Sidebar;
