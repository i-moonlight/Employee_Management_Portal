import React from 'react';
import Header from "../components/Header";
import Footer from "../components/Footer";

const Page = ({children}) => {
    return (
        <div>
            <Header />
            {/*<Sidebar />*/}
            <div>
                {children}
            </div>
            <Footer />
        </div>
    )
}

export default Page;
