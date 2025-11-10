require 'rails_helper'

RSpec.describe "/attendance_records", type: :request do

  let(:business) { Business.create!(name: "Test Business", contact_info: "+37060000000") }
  let(:activity) { Activity.create!(name: "Math", description: "Math class", is_active: true, business: business) }
  let(:student)  { Student.create!(full_name: "John Doe") }

  let(:valid_attributes) do
    {
      activity_id: activity.id,
      student_id: student.id
    }
  end

  let(:invalid_attributes) do
    {
      activity_id: nil,
      student_id: nil
    }
  end

  describe "GET /index" do
    it "renders a successful response" do
      AttendanceRecord.create! valid_attributes
      get attendance_records_url
      expect(response).to be_successful
    end
  end

  describe "GET /show" do
    it "renders a successful response" do
      attendance_record = AttendanceRecord.create! valid_attributes
      get attendance_record_url(attendance_record)
      expect(response).to be_successful
    end
  end

  describe "GET /new" do
    it "renders a successful response" do
      get new_attendance_record_url
      expect(response).to be_successful
    end
  end

  describe "GET /edit" do
    it "renders a successful response" do
      attendance_record = AttendanceRecord.create! valid_attributes
      get edit_attendance_record_url(attendance_record)
      expect(response).to be_successful
    end
  end

  describe "POST /create" do
    context "with valid parameters" do
      it "creates a new AttendanceRecord" do
        expect {
          post attendance_records_url, params: { attendance_record: valid_attributes }
        }.to change(AttendanceRecord, :count).by(1)
      end

      it "redirects to the created attendance_record" do
        post attendance_records_url, params: { attendance_record: valid_attributes }
        expect(response).to redirect_to(attendance_record_url(AttendanceRecord.last))
      end
    end

    context "with invalid parameters" do
      it "does not create a new AttendanceRecord" do
        expect {
          post attendance_records_url, params: { attendance_record: invalid_attributes }
        }.to change(AttendanceRecord, :count).by(0)
      end

      it "renders a response with 422 status (i.e. to display the 'new' template)" do
        post attendance_records_url, params: { attendance_record: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "PATCH /update" do
    context "with valid parameters" do
      let(:new_attributes) {
        skip("Add a hash of attributes valid for your model")
      }

      it "updates the requested attendance_record" do
        attendance_record = AttendanceRecord.create! valid_attributes
        patch attendance_record_url(attendance_record), params: { attendance_record: new_attributes }
        attendance_record.reload
        skip("Add assertions for updated state")
      end

      it "redirects to the attendance_record" do
        attendance_record = AttendanceRecord.create! valid_attributes
        patch attendance_record_url(attendance_record), params: { attendance_record: new_attributes }
        attendance_record.reload
        expect(response).to redirect_to(attendance_record_url(attendance_record))
      end
    end

    context "with invalid parameters" do
      it "renders a response with 422 status (i.e. to display the 'edit' template)" do
        attendance_record = AttendanceRecord.create! valid_attributes
        patch attendance_record_url(attendance_record), params: { attendance_record: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "DELETE /destroy" do
    it "destroys the requested attendance_record" do
      attendance_record = AttendanceRecord.create! valid_attributes
      expect {
        delete attendance_record_url(attendance_record)
      }.to change(AttendanceRecord, :count).by(-1)
    end

    it "redirects to the attendance_records list" do
      attendance_record = AttendanceRecord.create! valid_attributes
      delete attendance_record_url(attendance_record)
      expect(response).to redirect_to(attendance_records_url)
    end
  end
end
