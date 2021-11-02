import React from 'react';

import { Link } from 'react-router-dom';
import { Menu } from 'primereact/menu';

const Sidebar = () => {

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
    ]

    return (
        <div className="sidebar">
            <h3>Sidebar</h3>
            <Menu model={items} className={"sidebar"}/>
        </div>
    )
}

export default Sidebar;
